using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DHD.ENTITY
{
    public class SchoolNew
    {
        /// <summary>
        /// 新闻ID
        /// </summary>
         [DisplayName("新闻Id")]
        public int Id { get; set; }
        /// <summary>
        /// 新闻标题
        /// </summary>
        [Required]
        [DisplayName("新闻标题")]
        public string Title { get; set; }
        /// <summary>
        /// 新闻内容
        /// </summary>
        [Required]
        [DisplayName("新闻内容")]
        public string Content { get; set; }
        /// <summary>
        /// 新闻发布时间
        /// </summary>
         [DisplayName("发布时间")]
        public DateTime PublishTime { get; set; }
        /// <summary>
        /// 新闻作者
        /// </summary>
        [Required]
        [DisplayName("作者")]
        public string Author { get; set; }
        public int ViewNumber { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        [Range(0, 1)]
        public int IsDelete { get; set; }
        /// <summary>
        /// 对应学校的id
        /// </summary>
        public int SchoolId { get; set; }
        public int NewTypeId { get; set; }
        /// <summary>
        /// 判断文章有没有图片
        /// </summary>
        public int HaveImg { get; set; }
        /// <summary>
        /// 判断文章是否置顶，由管理员设置
        /// </summary>
        public int IsTop { get; set; }
        /// <summary>
        /// 判断图片是否已经抓取
        /// </summary>
        public int HasCatched { get; set; }
        public int ThumbUp { get; set; }
    }
}
