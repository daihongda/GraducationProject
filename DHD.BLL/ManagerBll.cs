using DHD.DAL;
using DHD.ENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DHD.BLL
{
    public class ManagerBll
    {
        public ManagerDal dal = new ManagerDal();
        /// <summary>
        /// 根据id获取学校的信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Manager Get(int id)
        {
            var Manager = dal.Get(id);
            return Manager;
        }
        /// <summary>
        /// 添加学校的信息
        /// </summary>
        /// <param name="newManager"></param>
        /// <returns></returns>
        public Manager Add(Manager newManager)
        {
            var Manager = dal.Add(newManager);
            return Manager;
        }
        /// <summary>
        /// 根据页数获取指定页面的列表信息
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="perPage"></param>
        /// <returns></returns>
        public List<Manager> GetByPage(int pageIndex, int perPage, int orderBy, int orderType)
        {
            var Managers = dal.GetByPage(orderBy, orderType).Skip((pageIndex - 1) * perPage).Take(perPage);
            return Managers.ToList<Manager>();
        }
        /// <summary>
        /// 根据输入的学校名称进行模糊查询
        /// </summary>
        /// <param name="ManagerName"></param>
        /// <param name="pageIndex"></param>
        /// <param name="perPage"></param>
        /// <param name="orderBy"></param>
        /// <param name="orderType"></param>
        /// <returns></returns>
        public List<Manager> SearchManager(string ManagerName, int pageIndex = 1, int perPage = 10, int orderBy = 0, int orderType = 0)
        {
            if (ManagerName == "")
            {
                return dal.GetByPage(orderBy, orderType).Skip((pageIndex - 1) * perPage).Take(perPage).ToList<Manager>();
            }
            var Managers = dal.GetByPage(orderBy, orderType).Where(d => d.UserName.IndexOf(ManagerName) > 0).Skip((pageIndex - 1) * perPage).Take(perPage);
            return Managers.ToList<Manager>();
        }
        public int AddRange(IEnumerable<Manager> Managers)
        {
            return dal.AddRange(Managers);
        }
        public int Delete(int id)
        {
            return dal.Delete(id);
        }
        public List<Manager> GetAll()
        {
            return dal.GetAll();
        }
        public int DeleteRange(string ids)
        {
            return dal.DeleteRange(ids);
        }
        public int Edit(Manager newManager)
        {
            return dal.Edit(newManager);
        }
    }
}
