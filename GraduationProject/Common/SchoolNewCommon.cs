using DHD.BLL;
using DHD.ENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace GraduationProject.Common
{
    public class SchoolNewCommon
    {
        public static List<NewType> getNewTypes()
        {
            var bll = new NewTypeBll();
            return bll.GetAll();
        }
        /// <summary>
        /// 可以根据省市获取一个树形结构，用于后台
        /// </summary>
        /// <returns></returns>
        public static List<School> getSchools()
        {
            var bll = new SchoolBll();
            return bll.GetAll();
        }
        ///// <summary>
        ///// 获取显示在下拉框的文件夹列表
        ///// </summary>
        //public static string GetSelectFoldertString(int type)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    List<School> listFolder = GetFolderByAndCurrentUserType(type);
        //    //sb.Append("[");
        //    listFolder.Insert(0, new School
        //    {
        //        Id = 0,
        //        Name = "省份"
        //    });
        //    GetSelectFolderString("-1", ref listFolder, ref sb);
        //    //var folder = listFolder.SingleOrDefault(n => n.F_PARENT_ID.Trim() == "0" || !listFolder.Exists(m => m.ID == n.F_PARENT_ID));
        //    //if (folder != null)
        //    //{
        //    //    GetSelectFolderString(folder.F_PARENT_ID.Trim(), ref listFolder, ref sb);
        //    //}


        //    //sb.Append(" ]");
        //    return sb.ToString();
        //}
        ///// <summary>
        ///// 递归节点
        ///// </summary>
        ///// <param name="parentId"></param>
        ///// <param name="listDept"></param>
        ///// <param name="sb"></param>
        //private static void GetSelectFolderString(string parentId, ref List<School> listFolder, ref StringBuilder sb)
        //{
        //    List<School> listFolderChild = listFolder.Where(a => a.F_PARENT_ID.Trim() == parentId).ToList();
        //    if (listFolderChild.Count > 0)
        //    {

        //        for (int i = 0; i < listFolderChild.Count; i++)
        //        {

        //            if (i > 0)
        //            {
        //                sb.Append(",");
        //            }
        //            sb.Append("{");
        //            sb.Append(string.Format(" text: '{0}',id:'{1}',parentId:'{2}'", listFolderChild[i].F_NAME, listFolderChild[i].ID, listFolderChild[i].F_PARENT_ID.Trim()));
        //            if (listFolder.Count(a => a.F_PARENT_ID == listFolderChild[i].ID) > 0)
        //            {
        //                sb.Append(",");
        //                sb.Append("nodes: [");

        //                GetSelectFolderString(listFolderChild[i].ID, ref listFolder, ref sb);

        //                sb.Append(" ]");
        //            }
        //            sb.Append("}");
        //        }

        //    }
        //}
    }
}