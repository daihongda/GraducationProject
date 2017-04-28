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
    public class ConfigureBll
    {
        public ConfigureDal dal = new ConfigureDal();
        /// <summary>
        /// 根据id获取学校的信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Configure Get(int id)
        {
            var Configure = dal.Get(id);
            return Configure;
        }
        /// <summary>
        /// 添加学校的信息
        /// </summary>
        /// <param name="newConfigure"></param>
        /// <returns></returns>
        public Configure Add(Configure newConfigure)
        {
            var Configure = dal.Add(newConfigure);
            return Configure;
        }
        /// <summary>
        /// 根据页数获取指定页面的列表信息
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="perPage"></param>
        /// <returns></returns>
        public List<Configure> GetByPage(int pageIndex, int perPage, int orderBy, int orderType)
        {
            var Configures = dal.GetByPage(orderBy, orderType).Skip((pageIndex - 1) * perPage).Take(perPage);
            return Configures.ToList<Configure>();
        }
        public List<Configure> SearchProvince(string searchContent, int pageIndex = 1, int perPage = 10, int orderBy = 0, int orderType = 0)
        {
            var Provinces = dal.GetByPage(orderBy, orderType, searchContent).Skip((pageIndex - 1) * perPage).Take(perPage);
            return Provinces.ToList<Configure>();
        }
        public int AddRange(IEnumerable<Configure> Configures)
        {
            return dal.AddRange(Configures);
        }
        public int Delete(int id)
        {
            return dal.Delete(id);
        }
        public List<Configure> GetAll()
        {
            return dal.GetAll();
        }
        public int DeleteRange(string ids)
        {
            return dal.DeleteRange(ids);
        }
        public int Edit(Configure newConfigure)
        {
            return dal.Edit(newConfigure);
        }
    }
}
