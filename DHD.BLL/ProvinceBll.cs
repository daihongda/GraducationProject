using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DHD.ENTITY;
using DHD.DAL;
namespace DHD.BLL
{
    public class ProvinceBll
    {
        public ProvinceDal dal = new ProvinceDal();
        /// <summary>
        /// 根据id获取学校的信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Province Get(int id)
        {
            var Province = dal.Get(id);
            return Province;
        }
        /// <summary>
        /// 添加学校的信息
        /// </summary>
        /// <param name="newProvince"></param>
        /// <returns></returns>
        public Province Add(Province newProvince)
        {
            var Province = dal.Add(newProvince);
            return Province;
        }
        /// <summary>
        /// 根据页数获取指定页面的列表信息
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="perPage"></param>
        /// <returns></returns>
        public List<Province> GetByPage(int pageIndex, int perPage, int orderBy, int orderType)
        {
            var Provinces = dal.GetByPage(orderBy, orderType).Skip((pageIndex - 1) * perPage).Take(perPage);
            return Provinces.ToList<Province>();
        }
        /// <summary>
        /// 根据输入的学校名称进行模糊查询
        /// </summary>
        /// <param name="ProvinceName"></param>
        /// <param name="pageIndex"></param>
        /// <param name="perPage"></param>
        /// <param name="orderBy"></param>
        /// <param name="orderType"></param>
        /// <returns></returns>
        public List<Province> SearchProvince(string searchContent, int pageIndex = 1, int perPage = 10, int orderBy = 0, int orderType = 0)
        {
            var Provinces = dal.GetByPage(orderBy, orderType, searchContent).Skip((pageIndex - 1) * perPage).Take(perPage);
            return Provinces.ToList<Province>();
        }
        public int AddRange(IEnumerable<Province> Provinces)
        {
            return dal.AddRange(Provinces);
        }
        public int Delete(int id)
        {
            return dal.Delete(id);
        }
        public List<Province> GetAll()
        {
            return dal.GetAll();
        }
        public int DeleteRange(string ids)
        {
            return dal.DeleteRange(ids);
        }
        public int Edit(Province newProvince)
        {
            return dal.Edit(newProvince);
        }
    }
}
