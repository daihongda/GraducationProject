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
    public class MenuItemDal
    {
        public DBContext db = new DBContext();
        public MenuItem Get(int id)
        {
            try
            {
                MenuItem MenuItem = db.MenuItems.Find(id);
                return MenuItem;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public MenuItem Add(MenuItem newMenuItem)
        {
            var MenuItem = db.MenuItems.Add(newMenuItem);
            db.SaveChanges();
            return MenuItem;
        }
        public int AddRange(IEnumerable<MenuItem> MenuItems)
        {
            db.MenuItems.AddRange(MenuItems);
            return db.SaveChanges();
        }
        public int Delete(int id)
        {
            var MenuItem = db.MenuItems.Find(id);
            MenuItem.IsDelete = 1;
            return db.SaveChanges();
        }
        public List<MenuItem> GetByPage(int orderColumn, int orderType)
        {
            var MenuItem = new MenuItem();
            string order = "";
            string column = "";
            if (orderType == 1)
            {
                order = "desc";
            }
            column = GetColumn<MenuItem>(orderColumn);
            string sql = string.Format("Select * from dbo.MenuItems order by {0} {1}", column, order);
            var MenuItems = db.Database.SqlQuery<MenuItem>(sql).ToList();
            return MenuItems;
        }
        public List<MenuItem> GetAll()
        {
            return db.MenuItems.ToList<MenuItem>();
        }
        public int DeleteRange(string ids)
        {
            List<MenuItem> MenuItems = new List<MenuItem>();
            string[] MenuItemArray = ids.Split(',');
            foreach (var id in MenuItemArray)
            {
                var MenuItem = db.MenuItems.Find(id);
                if (MenuItem != null)
                {
                    db.MenuItems.Remove(MenuItem);
                }
            }
            return db.SaveChanges();
        }
        public int Edit(MenuItem newMenuItem)
        {

            var MenuItem = db.MenuItems.Find(newMenuItem.Id);
            MenuItem.Name = newMenuItem.Name;
            MenuItem.ParentId = newMenuItem.ParentId;
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
