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
    public class School
    {
        /// <summary>
        /// 高校ID
        /// </summary>
        [DisplayName("学校id")]
        public int Id { get; set; }
        /// <summary>
        /// 高校名称
        /// </summary>
        [DisplayName("学校名称")]
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// 高校介绍
        /// </summary>
        [DisplayName("学校介绍")]
        [Required]
        public string Introduce { get; set; }
        /// <summary>
        /// 高校主页
        /// </summary>
        [DisplayName("学校主页")]
        [Required]
        public string HomePage { get; set; }
        public string SchoolIconUrl { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public int IsDelete { get; set; }
        public int AreaId { get; set; }
    }
}
