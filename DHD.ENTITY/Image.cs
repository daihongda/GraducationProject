using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace DHD.ENTITY
{
    public class Image
    {
        /// <summary>
        /// 图片id
        /// </summary>
        public int Id { get; set; }
        public int SchoolNewId { get; set; }
        /// <summary>
        /// 图片的存放路径
        /// </summary>
        public string Src { get; set; }
        /// <summary>
        /// 图片的创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }  
    }
}
