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
    public class NewTypeBll
    {
        public NewTypeDal dal = new NewTypeDal();
        /// <summary>
        /// 根据id获取学校的信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public NewType Get(int id)
        {
            var NewType = dal.Get(id);
            return NewType;
        }
        /// <summary>
        /// 添加学校的信息
        /// </summary>
        /// <param name="newNewType"></param>
        /// <returns></returns>
        public NewType Add(NewType newNewType)
        {
            var NewType = dal.Add(newNewType);
            return NewType;
        }
        /// <summary>
        /// 根据页数获取指定页面的列表信息
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="perPage"></param>
        /// <returns></returns>
        public List<NewType> GetByPage(int pageIndex, int perPage, int orderBy, int orderType)
        {
            var NewTypes = dal.GetByPage(orderBy, orderType).Skip((pageIndex - 1) * perPage).Take(perPage);
            return NewTypes.ToList<NewType>();
        }
        public List<NewType> SearchProvince(string searchContent, int pageIndex = 1, int perPage = 10, int orderBy = 0, int orderType = 0)
        {
            var Provinces = dal.GetByPage(orderBy, orderType, searchContent).Skip((pageIndex - 1) * perPage).Take(perPage);
            return Provinces.ToList<NewType>();
        }
        public int AddRange(IEnumerable<NewType> NewTypes)
        {
            return dal.AddRange(NewTypes);
        }
        public int Delete(int id)
        {
            return dal.Delete(id);
        }
        public List<NewType> GetAll()
        {
            return dal.GetAll();
        }
        public int DeleteRange(string ids)
        {
            return dal.DeleteRange(ids);
        }
        public int Edit(NewType newNewType)
        {
            return dal.Edit(newNewType);
        }
    }
}
