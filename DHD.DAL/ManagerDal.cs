using DHD.ENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DHD.DAL
{
    public class ManagerDal
    {
        public DBContext db = new DBContext();
        public Manager Get(int id)
        {
            try
            {
                Manager Manager = db.Managers.Find(id);
                return Manager;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public Manager Add(Manager newManager)
        {
            var Manager = db.Managers.Add(newManager);
            db.SaveChanges();
            return Manager;
        }
        public int AddRange(IEnumerable<Manager> Managers)
        {
            db.Managers.AddRange(Managers);
            return db.SaveChanges();
        }
        public int Delete(int id)
        {
            var Manager = db.Managers.Find(id);
            Manager.IsDelete = 1;
            return db.SaveChanges();
        }
        public List<Manager> GetByPage(int orderColumn, int orderType)
        {
            var Manager = new Manager();
            string order = "";
            string column = "";
            if (orderType == 1)
            {
                order = "desc";
            }
            column = GetColumn<Manager>(orderColumn);
            string sql = string.Format("Select * from dbo.Managers order by {0} {1}", column, order);
            var Managers = db.Database.SqlQuery<Manager>(sql).ToList();
            return Managers;
        }
        public List<Manager> GetAll()
        {
            return db.Managers.ToList<Manager>();
        }
        public int DeleteRange(string ids)
        {
            List<Manager> Managers = new List<Manager>();
            string[] ManagerArray = ids.Split(',');
            foreach (var id in ManagerArray)
            {
                var Manager = db.Managers.Find(int.Parse(id));
                if (Manager != null)
                {
                    db.Managers.Remove(Manager);
                }
            }
            return db.SaveChanges();
        }
        public int Edit(Manager newManager)
        {
            var Manager = db.Managers.Find(newManager.Id);
            Manager.NickName = newManager.NickName;
            Manager.Password = newManager.Password;
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
