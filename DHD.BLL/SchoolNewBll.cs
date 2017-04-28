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
    public class SchoolNewBll
    {
        public SchoolNewDal dal = new SchoolNewDal();
        /// <summary>
        /// 根据id获取学校的信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SchoolNew Get(int id)
        {
            var SchoolNew = dal.Get(id);
            return SchoolNew;
        }
        /// <summary>
        /// 添加学校的信息
        /// </summary>
        /// <param name="newSchoolNew"></param>
        /// <returns></returns>
        public SchoolNew Add(SchoolNew newSchoolNew)
        {
            var SchoolNew = dal.Add(newSchoolNew);
            return SchoolNew;
        }
        /// <summary>
        /// 根据页数获取指定页面的列表信息
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="perPage"></param>
        /// <returns></returns>
        public List<SchoolNew> GetByPage(int pageIndex, int perPage, int orderBy, int orderType,int schoolId=-1)
        {
            if (schoolId == -1)
            {
                var SchoolNews = dal.GetByPage(orderBy, orderType).Skip((pageIndex - 1) * perPage).Take(perPage);
                return SchoolNews.ToList<SchoolNew>();
            }
            else
            {
                var SchoolNews = dal.GetByPage(orderBy, orderType).Where(d=>d.SchoolId==schoolId).Skip((pageIndex - 1) * perPage).Take(perPage);
                return SchoolNews.ToList<SchoolNew>();
            }
        }
        public List<SchoolNew> SearchSchoolNew(string searchContent, int pageIndex = 1, int perPage = 10, int orderBy = 0, int orderType = 0)
        {
            var schools = dal.GetByPage(orderBy, orderType, searchContent).Skip((pageIndex - 1) * perPage).Take(perPage);
            return schools.ToList<SchoolNew>();
        }
        public int AddRange(IEnumerable<SchoolNew> SchoolNews)
        {
            return dal.AddRange(SchoolNews);
        }
        public int Delete(int id)
        {
            return dal.Delete(id);
        }
        public List<SchoolNew> GetAll()
        {
            return dal.GetAll();
        }
        public int DeleteRange(string ids)
        {
            return dal.DeleteRange(ids);
        }
        public int Edit(SchoolNew newSchoolNew)
        {
            return dal.Edit(newSchoolNew);
        }
    }
}
