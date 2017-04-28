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
    public class CommentDal
    {
        public DBContext db = new DBContext();
        public Comment Get(int id)
        {
            try
            {
                Comment Comment = db.Comments.Find(id);
                return Comment;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public Comment Add(Comment newComment)
        {
            var Comment = db.Comments.Add(newComment);
            db.SaveChanges();
            return Comment;
        }
        public int AddRange(IEnumerable<Comment> Comments)
        {
            db.Comments.AddRange(Comments);
            return db.SaveChanges();
        }
        public int Delete(int id)
        {
            var Comment = db.Comments.Find(id);
            Comment.IsDelete = 1;
            return db.SaveChanges();
        }
        public List<Comment> GetByPage(int orderColumn, int orderType)
        {
            var Comment = new Comment();
            string order = "";
            string column = "";
            if (orderType == 1)
            {
                order = "desc";
            }
            column = GetColumn<Comment>(orderColumn);
            string sql = string.Format("Select * from dbo.Comments order by {0} {1}", column, order);
            var Comments = db.Database.SqlQuery<Comment>(sql).ToList();
            return Comments;
        }
        public List<Comment> GetAll()
        {
            return db.Comments.ToList<Comment>();
        }
        public int DeleteRange(string ids)
        {
            List<Comment> Comments = new List<Comment>();
            string[] CommentArray = ids.Split(',');
            foreach (var id in CommentArray)
            {
                var Comment = db.Comments.Find(int.Parse(id));
                if (Comment != null)
                {
                    db.Comments.Remove(Comment);
                }
            }
            return db.SaveChanges();
        }
        public int Edit(Comment newComment)
        {

            var Comment = db.Comments.Find(newComment.Id);
            Comment.Title = newComment.Title;
            Comment.Content = newComment.Content;
            Comment.SchoolNewId = newComment.SchoolNewId;
            Comment.Author = newComment.Author;
            Comment.ParentId = newComment.ParentId;
            Comment.PublishTime = newComment.PublishTime;
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
