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
    public class SchoolNewDal
    {
        public DBContext db = new DBContext();
        public SchoolNew Get(int id)
        {
            try
            {
                SchoolNew SchoolNew = db.SchoolNews.Find(id);
                return SchoolNew;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public SchoolNew Add(SchoolNew newSchoolNew)
        {
            var SchoolNew = db.SchoolNews.Add(newSchoolNew);
            db.SaveChanges();
            return SchoolNew;
        }
        public int AddRange(IEnumerable<SchoolNew> SchoolNews)
        {
            db.SchoolNews.AddRange(SchoolNews);
            return db.SaveChanges();
        }
        public int Delete(int id)
        {
            var SchoolNew = db.SchoolNews.Find(id);
            SchoolNew.IsDelete = 1;
            return db.SaveChanges();
        }
        public List<SchoolNew> GetByPage(int orderColumn, int orderType, string searchContent = "")
        {
            var school = new SchoolNew();
            string order = "";
            string column = "";
            if (orderType == 1)
            {
                order = "desc";
            }
            column = GetColumn<School>(orderColumn);
            if (searchContent == "")
            {
                string sql = string.Format("Select * from dbo.SchoolNews order by {0} {1}", column, order);
                var schoolNews = db.Database.SqlQuery<SchoolNew>(sql).ToList();
                return schoolNews;
            }
            else
            {
                var title = searchContent.Split('=')[0];
                var content = searchContent.Split('=')[1];
                string sql = string.Format("Select * from dbo.SchoolNews where {0} like '%{1}%' order by {2} {3}", title, content, column, order);
                //string sql = string.Format("Select * from dbo.Schools where {0} like %{1}%", title, content);
                var schoolNews = db.Database.SqlQuery<SchoolNew>(sql).ToList();
                return schoolNews;
            }
        }
        public List<SchoolNew> GetAll()
        {
            return db.SchoolNews.ToList<SchoolNew>();
        }
        public int DeleteRange(string ids)
        {
            List<SchoolNew> SchoolNews = new List<SchoolNew>();
            string[] SchoolNewArray = ids.Split(',');
            foreach (var id in SchoolNewArray)
            {
                var SchoolNew = db.SchoolNews.Find(int.Parse(id));
                if (SchoolNew != null)
                {
                    db.SchoolNews.Remove(SchoolNew);
                }
            }
            return db.SaveChanges();
        }
        public int Edit(SchoolNew newSchoolNew)
        {

            var SchoolNew = db.SchoolNews.Find(newSchoolNew.Id);
            SchoolNew.Content = newSchoolNew.Content;
            SchoolNew.Title = newSchoolNew.Title;
            SchoolNew.Author = newSchoolNew.Author;
            SchoolNew.PublishTime = newSchoolNew.PublishTime;
            

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
