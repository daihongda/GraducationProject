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
    public class ConfigureDal
    {
        public DBContext db = new DBContext();
        public Configure Get(int id)
        {
            try
            {
                Configure Configure = db.Configures.Find(id);
                return Configure;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public Configure Add(Configure newConfigure)
        {
            var Configure = db.Configures.Add(newConfigure);
            db.SaveChanges();
            return Configure;
        }
        public int AddRange(IEnumerable<Configure> Configures)
        {
            db.Configures.AddRange(Configures);
            return db.SaveChanges();
        }
        public int Delete(int id)
        {
            var Configure = db.Configures.Find(id);
            Configure.IsDelete = 1;
            return db.SaveChanges();
        }
        public List<Configure> GetByPage(int orderColumn, int orderType, string searchContent = "")
        {
            string order = "";
            string column = "";
            if (orderType == 1)
            {
                order = "desc";
            }
            column = GetColumn<Configure>(orderColumn);
            if (searchContent == "")
            {
                string sql = string.Format("Select * from dbo.Configures order by {0} {1}", column, order);
                var newTypes = db.Database.SqlQuery<Configure>(sql).ToList();
                return newTypes;
            }
            else
            {
                var title = searchContent.Split('=')[0];
                var content = searchContent.Split('=')[1];
                string sql = string.Format("Select * from dbo.Configures where {0} like '%{1}%' order by {2} {3}", title, content, column, order);
                //string sql = string.Format("Select * from dbo.Schools where {0} like %{1}%", title, content);
                var newTypes = db.Database.SqlQuery<Configure>(sql).ToList();
                return newTypes;
            }
        }
        public List<Configure> GetAll()
        {
            return db.Configures.ToList<Configure>();
        }
        public int DeleteRange(string ids)
        {
            List<Configure> Configures = new List<Configure>();
            string[] ConfigureArray = ids.Split(',');
            foreach (var id in ConfigureArray)
            {
                var Configure = db.Configures.Find(int.Parse(id));
                if (Configure != null)
                {
                    db.Configures.Remove(Configure);
                }
            }
            return db.SaveChanges();
        }
        public int Edit(Configure newConfigure)
        {

            var Configure = db.Configures.Find(newConfigure.Id);
            Configure.ContentPath = newConfigure.ContentPath;
            Configure.HrefPath = newConfigure.HrefPath;
            Configure.ListPath = newConfigure.ListPath;
            Configure.SchoolId = newConfigure.SchoolId;
            Configure.TitlePath = newConfigure.TitlePath;
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
