using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DHD.ENTITY;
using System.Reflection;
namespace DHD.DAL
{
    public class AreaDal
    {
        public DBContext db = new DBContext();
        public Area Get(int id)
        {
            try
            {
                Area Area = db.Areas.Find(id);
                return Area;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public Area Add(Area newArea)
        {
            var Area = db.Areas.Add(newArea);
            db.SaveChanges();
            return Area;
        }
        public int AddRange(IEnumerable<Area> Areas)
        {
            db.Areas.AddRange(Areas);
            return db.SaveChanges();
        }
        public int Delete(int id)
        {
            var Area = db.Areas.Find(id);
            Area.IsDelete = 1;
            return db.SaveChanges();
        }
        public List<Area> GetByPage(int orderColumn, int orderType)
        {
            var Area = new Area();
            string order = "";
            string column = "";
            if (orderType == 1)
            {
                order = "desc";
            }
            column = GetColumn<Area>(orderColumn);
            string sql = string.Format("Select * from dbo.Areas order by {0} {1}", column, order);
            var Areas = db.Database.SqlQuery<Area>(sql).ToList();
            return Areas;
        }
        public List<Area> GetAll()
        {
            return db.Areas.ToList<Area>();
        }
        public int DeleteRange(string ids)
        {
            List<Area> Areas = new List<Area>();
            string[] AreaArray = ids.Split(',');
            foreach (var id in AreaArray)
            {
                var Area = db.Areas.Find(int.Parse(id));
                if (Area != null)
                {
                    db.Areas.Remove(Area);
                }
            }
            return db.SaveChanges();
        }
        public int Edit(Area newArea)
        {

            var Area = db.Areas.Find(newArea.Id);
            Area.Name = newArea.Name;
            return db.SaveChanges();
        }
        public static string GetColumn<T>(int orderColumn)
        {
            Type t = typeof(T);
            PropertyInfo[] PropertyList = t.GetProperties();
            string name = "id";
            for (int i = 0; i < PropertyList.Count(); i++)
            {
                if (orderColumn == i)
                {
                    name = PropertyList[i].Name;
                    break;
                }
            }
            return name;
        }
    }
}
