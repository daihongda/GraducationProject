using DHD.ENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DHD.DAL
{
    public class MessageDal
    {
        public DBContext db = new DBContext();
        public Message Get(int id)
        {
            try
            {
                Message Message = db.Messages.Find(id);
                return Message;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public Message Add(Message newMessage)
        {
            var Message = db.Messages.Add(newMessage);
            db.SaveChanges();
            return Message;
        }
        public int AddRange(IEnumerable<Message> Messages)
        {
            db.Messages.AddRange(Messages);
            return db.SaveChanges();
        }
        public int Delete(int id)
        {
            var Message = db.Messages.Find(id);
            //Message.IsDelete = 1;
            return db.SaveChanges();
        }
        public List<Message> GetByPage(int orderColumn, int orderType, string searchContent = "")
        {
            string order = "";
            string column = "";
            if (orderType == 1)
            {
                order = "desc";
            }
            column = GetColumn<Message>(orderColumn);
            if (searchContent == "")
            {
                string sql = string.Format("Select * from dbo.Messages order by {0} {1}", column, order);
                var newTypes = db.Database.SqlQuery<Message>(sql).ToList();
                return newTypes;
            }
            else
            {
                var title = searchContent.Split('=')[0];
                var content = searchContent.Split('=')[1];
                string sql = string.Format("Select * from dbo.Messages where {0} like '%{1}%' order by {2} {3}", title, content, column, order);
                //string sql = string.Format("Select * from dbo.Schools where {0} like %{1}%", title, content);
                var newTypes = db.Database.SqlQuery<Message>(sql).ToList();
                return newTypes;
            }
        }
        public List<Message> GetAll()
        {
            return db.Messages.ToList<Message>();
        }
        public int DeleteRange(string ids)
        {
            List<Message> Messages = new List<Message>();
            string[] MessageArray = ids.Split(',');
            foreach (var id in MessageArray)
            {
                var Message = db.Messages.Find(int.Parse(id));
                if (Message != null)
                {
                    db.Messages.Remove(Message);
                }
            }
            return db.SaveChanges();
        }
        public int Edit(Message newMessage)
        {

            var Message = db.Messages.Find(newMessage.Id);
            Message.Title = newMessage.Title;
            Message.Content = newMessage.Content;
            Message.To = newMessage.To;
            Message.From = newMessage.From;
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
