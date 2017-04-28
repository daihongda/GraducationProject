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
    public class MenuItemBll
    {
        public MenuItemDal dal = new MenuItemDal();
        /// <summary>
        /// 根据id获取学校的信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MenuItem Get(int id)
        {
            var MenuItem = dal.Get(id);
            return MenuItem;
        }
        /// <summary>
        /// 添加学校的信息
        /// </summary>
        /// <param name="newMenuItem"></param>
        /// <returns></returns>
        public MenuItem Add(MenuItem newMenuItem)
        {
            var MenuItem = dal.Add(newMenuItem);
            return MenuItem;
        }
        /// <summary>
        /// 根据页数获取指定页面的列表信息
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="perPage"></param>
        /// <returns></returns>
        public List<MenuItem> GetByPage(int pageIndex, int perPage, int orderBy, int orderType)
        {
            var MenuItems = dal.GetByPage(orderBy, orderType).Skip((pageIndex - 1) * perPage).Take(perPage);
            return MenuItems.ToList<MenuItem>();
        }
        public int AddRange(IEnumerable<MenuItem> MenuItems)
        {
            return dal.AddRange(MenuItems);
        }
        public int Delete(int id)
        {
            return dal.Delete(id);
        }
        public List<MenuItem> GetAll()
        {
            return dal.GetAll();
        }
        public int DeleteRange(string ids)
        {
            return dal.DeleteRange(ids);
        }
        public int Edit(MenuItem newMenuItem)
        {
            return dal.Edit(newMenuItem);
        }
    }
}
