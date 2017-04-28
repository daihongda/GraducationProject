using DHD.DAL;
using DHD.ENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DHD.BLL
{
    public class UserBll
    {
        public UserDal dal = new UserDal();
        /// <summary>
        /// 根据id获取学校的信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public User Get(int id)
        {
            var User = dal.Get(id);
            return User;
        }
        /// <summary>
        /// 添加学校的信息
        /// </summary>
        /// <param name="newUser"></param>
        /// <returns></returns>
        public User Add(User newUser)
        {
            var User = dal.Add(newUser);
            return User;
        }
        /// <summary>
        /// 根据页数获取指定页面的列表信息
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="perPage"></param>
        /// <returns></returns>
        public List<User> GetByPage(int pageIndex, int perPage, int orderBy, int orderType)
        {
            var Users = dal.GetByPage(orderBy, orderType).Skip((pageIndex - 1) * perPage).Take(perPage);
            return Users.ToList<User>();
        }
        /// <summary>
        /// 根据输入的学校名称进行模糊查询
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="pageIndex"></param>
        /// <param name="perPage"></param>
        /// <param name="orderBy"></param>
        /// <param name="orderType"></param>
        /// <returns></returns>
        public List<User> SearchUser(string UserName, int pageIndex = 1, int perPage = 10, int orderBy = 0, int orderType = 0)
        {
            if (UserName == "")
            {
                return dal.GetByPage(orderBy, orderType).Skip((pageIndex - 1) * perPage).Take(perPage).ToList<User>();
            }
            var Users = dal.GetByPage(orderBy, orderType).Where(d => d.UserName.IndexOf(UserName) > 0).Skip((pageIndex - 1) * perPage).Take(perPage);
            return Users.ToList<User>();
        }
        public int AddRange(IEnumerable<User> Users)
        {
            return dal.AddRange(Users);
        }
        public int Delete(int id)
        {
            return dal.Delete(id);
        }
        public List<User> GetAll()
        {
            return dal.GetAll();
        }
        public int DeleteRange(string ids)
        {
            return dal.DeleteRange(ids);
        }
        public int Edit(User newUser)
        {
            return dal.Edit(newUser);
        }
    }
}
