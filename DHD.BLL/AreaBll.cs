using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DHD.ENTITY;
using DHD.DAL;
namespace DHD.BLL
{
    public class AreaBll
    {
        public AreaDal dal = new AreaDal();
        /// <summary>
        /// 根据id获取学校的信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Area Get(int id)
        {
            var Area = dal.Get(id);
            return Area;
        }
        /// <summary>
        /// 添加学校的信息
        /// </summary>
        /// <param name="newArea"></param>
        /// <returns></returns>
        public Area Add(Area newArea)
        {
            var Area = dal.Add(newArea);
            return Area;
        }
        /// <summary>
        /// 根据页数获取指定页面的列表信息
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="perPage"></param>
        /// <returns></returns>
        public List<Area> GetByPage(int pageIndex, int perPage, int orderBy, int orderType)
        {
            var Areas = dal.GetByPage(orderBy, orderType).Skip((pageIndex - 1) * perPage).Take(perPage);
            return Areas.ToList<Area>();
        }
        /// <summary>
        /// 根据输入的学校名称进行模糊查询
        /// </summary>
        /// <param name="AreaName"></param>
        /// <param name="pageIndex"></param>
        /// <param name="perPage"></param>
        /// <param name="orderBy"></param>
        /// <param name="orderType"></param>
        /// <returns></returns>
        public List<Area> SearchArea(string AreaName, int pageIndex = 1, int perPage = 10, int orderBy = 0, int orderType = 0)
        {
            if (AreaName == "")
            {
                return dal.GetByPage(orderBy, orderType).Skip((pageIndex - 1) * perPage).Take(perPage).ToList<Area>();
            }
            var Areas = dal.GetByPage(orderBy, orderType).Where(d => d.Name.IndexOf(AreaName) > 0).Skip((pageIndex - 1) * perPage).Take(perPage);
            return Areas.ToList<Area>();
        }
        public int AddRange(IEnumerable<Area> Areas)
        {
            return dal.AddRange(Areas);
        }
        public int Delete(int id)
        {
            return dal.Delete(id);
        }
        public List<Area> GetAll()
        {
            return dal.GetAll();
        }
        public int DeleteRange(string ids)
        {
            return dal.DeleteRange(ids);
        }
        public int Edit(Area newArea)
        {
            return dal.Edit(newArea);
        }
    }
}
