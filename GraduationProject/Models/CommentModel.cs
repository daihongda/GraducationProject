using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DHD.ENTITY;
namespace GraduationProject.Models
{
    public class CommentModel
    {
        /// <summary>
        /// 评论ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 评论标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 评论内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 评论发布时间
        /// </summary>
        public string PublishTime { get; set; }
        /// <summary>
        /// 评论作者
        /// </summary>
        public string Author { get; set; }
        public int UserId { get; set; }
        /// <summary>
        /// 高校新闻ID
        /// </summary>
        public int SchoolNewId { get; set; }
        /// <summary>
        /// 父级评论ID
        /// </summary>
        public int ParentId { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public int IsDelete { get; set; }
        /// <summary>
        /// 点赞数
        /// </summary>
        public int ThumbUp { get; set; }
        public static CommentModel Transfer(Comment comment){
            CommentModel model = new CommentModel();
            model.Id = comment.Id;
            model.IsDelete = comment.IsDelete;
            model.ParentId = comment.ParentId;
            model.SchoolNewId = comment.SchoolNewId;
            model.PublishTime = publishDateTransfer(comment.PublishTime);
            model.Content = comment.Content;
            model.ThumbUp = comment.ThumbUp;
            model.Author = comment.Author;
            model.UserId = comment.UserId;
            return model;
        }
        public static string publishDateTransfer(DateTime datetime){
            DateTime now = DateTime.Now;
            TimeSpan timeSpan = now.Subtract(datetime);
            if (timeSpan.Days > 0 )
            {
                if(timeSpan.Days<=3)
                return timeSpan.Days + "天前";
                else
                return datetime.ToShortDateString();
            }
            else if(timeSpan.Hours>0){
                return timeSpan.Hours + "小时前";
            }
            else if (timeSpan.Minutes > 0)
            {
                return timeSpan.Minutes + "分钟前";
            }
            else if (timeSpan.Seconds > 0)
            {
                return timeSpan.Seconds + "秒前";
            }
            else
            {
                return "刚刚";
            }
        }
    }
}