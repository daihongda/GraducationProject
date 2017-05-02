using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DHD.ENTITY;
using DHD.DAL;
using System.Reflection;
using System.Linq.Expressions;
namespace DHD.BLL
{
    public class MessageBll
    {
        public MessageDal dal = new MessageDal();
        /// <summary>
        /// 根据id获取学校的信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Message Get(int id)
        {
            var Message = dal.Get(id);
            return Message;
        }
        /// <summary>
        /// 添加学校的信息
        /// </summary>
        /// <param name="newMessage"></param>
        /// <returns></returns>
        public Message Add(Message newMessage)
        {
            var Message = dal.Add(newMessage);
            return Message;
        }
        /// <summary>
        /// 根据页数获取指定页面的列表信息
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="perPage"></param>
        /// <returns></returns>
        public List<Message> GetByPage(int pageIndex, int perPage, int orderBy, int orderType)
        {
            var Messages = dal.GetByPage(orderBy, orderType).Skip((pageIndex - 1) * perPage).Take(perPage);
            return Messages.ToList<Message>();
        }
        public List<Message> SearchMessage(string searchContent, int pageIndex = 1, int perPage = 10, int orderBy = 0, int orderType = 0)
        {
            var Provinces = dal.GetByPage(orderBy, orderType, searchContent).Skip((pageIndex - 1) * perPage).Take(perPage);
            return Provinces.ToList<Message>();
        }
        public List<Message> SearchMessages(string searchContent, int pageIndex = 1, int perPage = 10, int orderBy = 0, int orderType = 0)
        {
            var Provinces = dal.GetByPage(orderBy, orderType, searchContent).Skip((pageIndex - 1) * perPage).Take(perPage);
            return Provinces.ToList<Message>();
        }
        public int AddRange(IEnumerable<Message> Messages)
        {
            return dal.AddRange(Messages);
        }
        public int Delete(int id)
        {
            return dal.Delete(id);
        }
        public List<Message> GetAll()
        {
            return dal.GetAll();
        }
        public int DeleteRange(string ids)
        {
            return dal.DeleteRange(ids);
        }
        public int Edit(Message newMessage)
        {
            return dal.Edit(newMessage);
        }
    }
}
