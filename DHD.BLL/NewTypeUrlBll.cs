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
    public class NewTypeUrlBll
    {
        public NewTypeUrlDal dal = new NewTypeUrlDal();
        /// <summary>
        /// 根据id获取学校的信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public NewTypeUrl Get(int id)
        {
            var NewTypeUrl = dal.Get(id);
            return NewTypeUrl;
        }
        /// <summary>
        /// 添加学校的信息
        /// </summary>
        /// <param name="newNewTypeUrl"></param>
        /// <returns></returns>
        public NewTypeUrl Add(NewTypeUrl newNewTypeUrl)
        {
            var NewTypeUrl = dal.Add(newNewTypeUrl);
            return NewTypeUrl;
        }
        /// <summary>
        /// 根据页数获取指定页面的列表信息
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="perPage"></param>
        /// <returns></returns>
        public List<NewTypeUrl> GetByPage(int pageIndex, int perPage, int orderBy, int orderType)
        {
            var NewTypeUrls = dal.GetByPage(orderBy, orderType).Skip((pageIndex - 1) * perPage).Take(perPage);
            return NewTypeUrls.ToList<NewTypeUrl>();
        }
        public List<NewTypeUrl> SearchProvince(string searchContent, int pageIndex = 1, int perPage = 10, int orderBy = 0, int orderType = 0)
        {
            var Provinces = dal.GetByPage(orderBy, orderType, searchContent).Skip((pageIndex - 1) * perPage).Take(perPage);
            return Provinces.ToList<NewTypeUrl>();
        }
        public int AddRange(IEnumerable<NewTypeUrl> NewTypeUrls)
        {
            return dal.AddRange(NewTypeUrls);
        }
        public int Delete(int id)
        {
            return dal.Delete(id);
        }
        public List<NewTypeUrl> GetAll()
        {
            return dal.GetAll();
        }
        public int DeleteRange(string ids)
        {
            return dal.DeleteRange(ids);
        }
        public int Edit(NewTypeUrl newNewTypeUrl)
        {
            return dal.Edit(newNewTypeUrl);
        }
    }
}
