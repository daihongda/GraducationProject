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
    public class CommentBll
    {
        public CommentDal dal = new CommentDal();
        /// <summary>
        /// 根据id获取学校的信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Comment Get(int id)
        {
            var Comment = dal.Get(id);
            return Comment;
        }
        /// <summary>
        /// 添加学校的信息
        /// </summary>
        /// <param name="newComment"></param>
        /// <returns></returns>
        public Comment Add(Comment newComment)
        {
            var Comment = dal.Add(newComment);
            return Comment;
        }
        /// <summary>
        /// 根据页数获取指定页面的列表信息
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="perPage"></param>
        /// <returns></returns>
        public List<Comment> GetByPage(int pageIndex, int perPage, int orderBy, int orderType)
        {
            var Comments = dal.GetByPage(orderBy, orderType).Skip((pageIndex - 1) * perPage).Take(perPage);
            return Comments.ToList<Comment>();
        }
        public int AddRange(IEnumerable<Comment> Comments)
        {
            return dal.AddRange(Comments);
        }
        public int Delete(int id)
        {
            return dal.Delete(id);
        }
        public List<Comment> GetAll()
        {
            return dal.GetAll();
        }
        public int DeleteRange(string ids)
        {
            return dal.DeleteRange(ids);
        }
        public int Edit(Comment newComment)
        {
            return dal.Edit(newComment);
        }
    }
}
