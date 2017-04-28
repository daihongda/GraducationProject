using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DHD.ENTITY;
using System.Reflection;
namespace DHD.DAL
{
    public class ProvinceDal
    {
        public DBContext db = new DBContext();
        public Province Get(int id)
        {
            try
            {
                Province Province = db.Provinces.Find(id);
                return Province;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public Province Add(Province newProvince)
        {
            var Province = db.Provinces.Add(newProvince);
            db.SaveChanges();
            return Province;
        }
        public int AddRange(IEnumerable<Province> Provinces)
        {
            db.Provinces.AddRange(Provinces);
            return db.SaveChanges();
        }
        public int Delete(int id)
        {
            var Province = db.Provinces.Find(id);
            //Province.IsDelete = 1;
            return db.SaveChanges();
        }
        public List<Province> GetByPage(int orderColumn, int orderType, string searchContent = "")
        {
            var province = new Province();
            string order = "";
            string column = "";
            if (orderType == 1)
            {
                order = "desc";
            }
            column = GetColumn<Province>(orderColumn);
            if (searchContent == "")
            {
                string sql = string.Format("Select * from dbo.Provinces order by {0} {1}", column, order);
                var provinces = db.Database.SqlQuery<Province>(sql).ToList();
                return provinces;
            }
            else
            {
                var title = searchContent.Split('=')[0];
                var content = searchContent.Split('=')[1];
                string sql = string.Format("Select * from dbo.Provinces where {0} like '%{1}%' order by {2} {3}", title, content, column, order);
                //string sql = string.Format("Select * from dbo.Schools where {0} like %{1}%", title, content);
                var provinces = db.Database.SqlQuery<Province>(sql).ToList();
                return provinces;
            }
        }
        public List<Province> GetAll()
        {
            return db.Provinces.ToList<Province>();
        }
        public int DeleteRange(string ids)
        {
            List<Province> Provinces = new List<Province>();
            string[] ProvinceArray = ids.Split(',');
            foreach (var id in ProvinceArray)
            {
                var Province = db.Provinces.Find(int.Parse(id));
                if (Province != null)
                {
                    db.Provinces.Remove(Province);
                }
            }
            return db.SaveChanges();
        }
        public int Edit(Province newProvince)
        {

            var Province = db.Provinces.Find(newProvince.Id);
            Province.Name = newProvince.Name;
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
