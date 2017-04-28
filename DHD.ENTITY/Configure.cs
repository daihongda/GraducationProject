using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace DHD.ENTITY
{
    public class Configure
    {
        /// <summary>
        /// 配置ID
        /// </summary>
        [DisplayName("配置Id")]
        public int Id { get; set; }
        /// <summary>
        /// 新闻列表路径
        /// </summary>
        [DisplayName("抓取路径")]
        public string ListPath { get; set; }
        /// <summary>
        /// 新闻链接路径
        /// </summary>
        [DisplayName("链接路径")]
        public string HrefPath { get; set; }
        /// <summary>
        /// 新闻标题路径
        /// </summary>
         [DisplayName("标题路径")]
        public string TitlePath { get; set; }
        /// <summary>
        /// 新闻发表时间路径
        /// </summary>
         [DisplayName("时间路径")]
        public string TimePath { get; set; }
        /// <summary>
        /// 新闻内容路径
        /// </summary>
         [DisplayName("内容路径")]
        public string ContentPath { get; set; }
        /// <summary>
        /// 新闻作者路径
        /// </summary>
         [DisplayName("作者路径")]
        public string AuthorPath { get; set; }
        /// <summary>
        /// 高校ID
        /// </summary>
         [DisplayName("学校名称")]
        public int SchoolId { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public int IsDelete { get; set; }
        /// <summary>
        /// 下一页的地址格式，主要是因为很多学校的页面的分页栏都是加载出来，所以htmlaglitypack无法解析
        /// </summary>
         [DisplayName("下一页地址的模型")]
        public string NextPageModule { get; set; }
    }
}
