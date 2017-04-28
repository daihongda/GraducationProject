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
    public class NewTypeUrl
    {
        /// <summary>
        /// 高校ID
        /// </summary>
        [Key, Column(Order = 1)]
        [DisplayName("学校Id")]
        public int SchoolId { get; set; }
        /// <summary>
        /// 新闻类型ID
        /// </summary>
        [Key, Column(Order = 2)]
        [DisplayName("新闻分类Id")]
        public int NewTypeId { get; set; }
        /// <summary>
        /// 高校对应新闻分类的地址
        /// </summary>
         [DisplayName("页面地址")]
        public string Url { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public int IsDelete { get; set; }
    }
}
