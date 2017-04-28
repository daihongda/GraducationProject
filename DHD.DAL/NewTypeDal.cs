using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DHD.ENTITY;
using System.Data.SqlClient;
using System.Reflection;
namespace DHD.DAL
{
    public class NewTypeDal
    {
        public DBContext db = new DBContext();
        public NewType Get(int id)
        {
            try
            {
                NewType NewType = db.NewTypes.Find(id);
                return NewType;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public NewType Add(NewType newNewType)
        {
            var NewType = db.NewTypes.Add(newNewType);
            db.SaveChanges();
            return NewType;
        }
        public int AddRange(IEnumerable<NewType> NewTypes)
        {
            db.NewTypes.AddRange(NewTypes);
            return db.SaveChanges();
        }
        public int Delete(int id)
        {
            var NewType = db.NewTypes.Find(id);
            NewType.IsDelete = 1;
            return db.SaveChanges();
        }
        public List<NewType> GetByPage(int orderColumn, int orderType, string searchContent = "")
        {
            string order = "";
            string column = "";
            if (orderType == 1)
            {
                order = "desc";
            }
            column = GetColumn<NewType>(orderColumn);
            if (searchContent == "")
            {
                string sql = string.Format("Select * from dbo.NewTypes order by {0} {1}", column, order);
                var newTypes = db.Database.SqlQuery<NewType>(sql).ToList();
                return newTypes;
            }
            else
            {
                var title = searchContent.Split('=')[0];
                var content = searchContent.Split('=')[1];
                string sql = string.Format("Select * from dbo.NewTypes where {0} like '%{1}%' order by {2} {3}", title, content, column, order);
                //string sql = string.Format("Select * from dbo.Schools where {0} like %{1}%", title, content);
                var newTypes = db.Database.SqlQuery<NewType>(sql).ToList();
                return newTypes;
            }
        }
        public List<NewType> GetAll()
        {
            return db.NewTypes.ToList<NewType>();
        }
        public int DeleteRange(string ids)
        {
            List<NewType> NewTypes = new List<NewType>();
            string[] NewTypeArray = ids.Split(',');
            foreach (var id in NewTypeArray)
            {
                var NewType = db.NewTypes.Find(int.Parse(id));
                if (NewType != null)
                {
                    db.NewTypes.Remove(NewType);
                }
            }
            return db.SaveChanges();
        }
        public int Edit(NewType newNewType)
        {

            var NewType = db.NewTypes.Find(newNewType.Id);
            NewType.Name = newNewType.Name;
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
