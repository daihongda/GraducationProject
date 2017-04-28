using DHD.ENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DHD.DAL
{
    public class UserDal
    {
        public DBContext db = new DBContext();
        public User Get(int id)
        {
            try
            {
                User User = db.Users.Find(id);
                return User;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public User Add(User newUser)
        {
            var User = db.Users.Add(newUser);
            db.SaveChanges();
            return User;
        }
        public int AddRange(IEnumerable<User> Users)
        {
            db.Users.AddRange(Users);
            return db.SaveChanges();
        }
        public int Delete(int id)
        {
            var User = db.Users.Find(id);
            User.IsDelete = 1;
            return db.SaveChanges();
        }
        public List<User> GetByPage(int orderColumn, int orderType)
        {
            var User = new User();
            string order = "";
            string column = "";
            if (orderType == 1)
            {
                order = "desc";
            }
            column = GetColumn<User>(orderColumn);
            string sql = string.Format("Select * from dbo.Users order by {0} {1}", column, order);
            var Users = db.Database.SqlQuery<User>(sql).ToList();
            return Users;
        }
        public List<User> GetAll()
        {
            return db.Users.ToList<User>();
        }
        public int DeleteRange(string ids)
        {
            List<User> Users = new List<User>();
            string[] UserArray = ids.Split(',');
            foreach (var id in UserArray)
            {
                var User = db.Users.Find(id);
                if (User != null)
                {
                    db.Users.Remove(User);
                }
            }
            return db.SaveChanges();
        }
        public int Edit(User newUser)
        {

            var User = db.Users.Find(newUser.Id);
            User.NickName = newUser.NickName;
            User.Password = newUser.Password;
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
