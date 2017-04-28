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
    public class SchoolDal
    {
        public DBContext db = new DBContext();
        public School Get(int id)
        {
            try
            {
                School School = db.Schools.Find(id);
                return School;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public School Add(School newSchool)
        {
            var School = db.Schools.Add(newSchool);
            db.SaveChanges();
            return School;
        }
        public int AddRange(IEnumerable<School> Schools)
        {
            db.Schools.AddRange(Schools);
            return db.SaveChanges();
        }
        public int Delete(int id)
        {
            var School = db.Schools.Find(id);
            School.IsDelete = 1;
            return db.SaveChanges();
        }
        public List<School> GetByPage(int orderColumn, int orderType,string searchContent="")
        {
            var school = new School();
            string order = "";
            string column = "";
            if (orderType == 1)
            {
                order = "desc";
            }
            column = GetColumn<School>(orderColumn);
            if (searchContent == "")
            {
                string sql = string.Format("Select * from dbo.Schools order by {0} {1}", column, order);
                var schools = db.Database.SqlQuery<School>(sql).ToList();
                return schools;
            }
            else
            {
                var title = searchContent.Split('=')[0];
                var content = searchContent.Split('=')[1];
                string sql = string.Format("Select * from dbo.Schools where {0} like '%{1}%' order by {2} {3}",title,content,column, order);
                //string sql = string.Format("Select * from dbo.Schools where {0} like %{1}%", title, content);
                var schools = db.Database.SqlQuery<School>(sql).ToList();
                return schools;
            }
        }
        public List<School> GetAll()
        {
            return db.Schools.ToList<School>();
        }
        public int DeleteRange(string ids)
        {
            List<School> Schools = new List<School>();
            string[] SchoolArray = ids.Split(',');
            foreach (var id in SchoolArray)
            {
                var School = db.Schools.Find(int.Parse(id));
                if (School != null)
                {
                    School.IsDelete = 1;
                }
            }
            return db.SaveChanges();
        }
        public int Edit(School newSchool)
        {

            var school = db.Schools.Find(newSchool.Id);
            school.HomePage = newSchool.HomePage;
            school.Introduce = newSchool.Introduce;
            school.Name = newSchool.Name;
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
