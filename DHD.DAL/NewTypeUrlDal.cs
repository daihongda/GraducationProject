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
    public class NewTypeUrlDal
    {
        public DBContext db = new DBContext();
        public NewTypeUrl Get(int id)
        {
            try
            {
                NewTypeUrl NewTypeUrl = db.NewTypeUrls.Find(id);
                return NewTypeUrl;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public NewTypeUrl Add(NewTypeUrl newNewTypeUrl)
        {
            var NewTypeUrl = db.NewTypeUrls.Add(newNewTypeUrl);
            db.SaveChanges();
            return NewTypeUrl;
        }
        public int AddRange(IEnumerable<NewTypeUrl> NewTypeUrls)
        {
            db.NewTypeUrls.AddRange(NewTypeUrls);
            return db.SaveChanges();
        }
        public int Delete(int id)
        {
            var NewTypeUrl = db.NewTypeUrls.Find(id);
            NewTypeUrl.IsDelete = 1;
            return db.SaveChanges();
        }
        public List<NewTypeUrl> GetByPage(int orderColumn, int orderType, string searchContent = "")
        {
            string order = "";
            string column = "";
            if (orderType == 1)
            {
                order = "desc";
            }
            column = GetColumn<NewTypeUrl>(orderColumn);
            if (searchContent == "")
            {
                string sql = string.Format("Select * from dbo.NewTypeUrls order by {0} {1}", column, order);
                var NewTypeUrls = db.Database.SqlQuery<NewTypeUrl>(sql).ToList();
                return NewTypeUrls;
            }
            else
            {
                var title = searchContent.Split('=')[0];
                var content = searchContent.Split('=')[1];
                string sql = string.Format("Select * from dbo.NewTypeUrls where {0} like '%{1}%' order by {2} {3}", title, content, column, order);
                //string sql = string.Format("Select * from dbo.Schools where {0} like %{1}%", title, content);
                var NewTypeUrls = db.Database.SqlQuery<NewTypeUrl>(sql).ToList();
                return NewTypeUrls;
            }
        }
        public List<NewTypeUrl> GetAll()
        {
            return db.NewTypeUrls.ToList<NewTypeUrl>();
        }
        public int DeleteRange(string ids)
        {
            List<NewTypeUrl> NewTypeUrls = new List<NewTypeUrl>();
            string[] NewTypeUrlArray = ids.Split(',');
            foreach (var id in NewTypeUrlArray)
            {
                var NewTypeUrl = db.NewTypeUrls.Find(int.Parse(id));
                if (NewTypeUrl != null)
                {
                    db.NewTypeUrls.Remove(NewTypeUrl);
                }
            }
            return db.SaveChanges();
        }
        public int Edit(NewTypeUrl newNewTypeUrl)
        {

            var NewTypeUrl = db.NewTypeUrls.Where(d => d.NewTypeId == newNewTypeUrl.NewTypeId).Where(d => d.SchoolId == newNewTypeUrl.SchoolId).FirstOrDefault();
            NewTypeUrl.Url = newNewTypeUrl.Url;
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
