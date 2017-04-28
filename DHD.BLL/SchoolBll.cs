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
    public class SchoolBll
    {
        public SchoolDal dal = new SchoolDal();
        /// <summary>
        /// 根据id获取学校的信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public School Get(int id)
        {
            var School = dal.Get(id);
            return School;
        }
        /// <summary>
        /// 添加学校的信息
        /// </summary>
        /// <param name="newSchool"></param>
        /// <returns></returns>
        public School Add(School newSchool)
        {
            var School = dal.Add(newSchool);
            return School;
        }
        /// <summary>
        /// 根据页数获取指定页面的列表信息
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="perPage"></param>
        /// <returns></returns>
        public List<School> GetByPage(int pageIndex, int perPage, int orderBy, int orderType)
        {
            var schools = dal.GetByPage(orderBy, orderType).Skip((pageIndex - 1) * perPage).Take(perPage);
            return schools.ToList<School>();
        }
        /// <summary>
        /// 根据输入的学校名称进行模糊查询
        /// </summary>
        /// <param name="schoolName"></param>
        /// <param name="pageIndex"></param>
        /// <param name="perPage"></param>
        /// <param name="orderBy"></param>
        /// <param name="orderType"></param>
        /// <returns></returns>
        public List<School> SearchSchool(string searchContent,int pageIndex=1, int perPage=10, int orderBy=0, int orderType=0)
        {
            var schools = dal.GetByPage(orderBy, orderType,searchContent).Skip((pageIndex - 1) * perPage).Take(perPage);
            return schools.ToList<School>();
        }
        public int AddRange(IEnumerable<School> Schools)
        {
            return dal.AddRange(Schools);
        }
        public int Delete(int id)
        {
            return dal.Delete(id);
        }
        public List<School> GetAll()
        {
            return dal.GetAll();
        }
        public int DeleteRange(string ids)
        {
            return dal.DeleteRange(ids);
        }
        public int Edit(School newSchool)
        {
            return dal.Edit(newSchool);
        }
    }
}
