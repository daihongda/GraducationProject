using DHD.ENTITY;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GraduationProject.Models
{
    public class SchoolNewModel
    {
        /// <summary>
        /// 新闻ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 新闻标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 新闻内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 新闻发布时间
        /// </summary>
        public string PublishTime { get; set; }
        /// <summary>
        /// 新闻作者
        /// </summary>
        public string Author { get; set; }
        //public List<string> Images { get; set; }
        public string Images { get; set; }
        public string SchoolName { get; set; }
        public string NewTypeName { get; set; }
        public List<Comment> Comments { get; set; }
        public int ViewNumber { get; set; }
        public int ThumbUp { get; set; }
        public int CommentNumber { get; set; }
        public static SchoolNewModel Transfer(SchoolNew schoolNew)
        {
            SchoolNewModel model = new SchoolNewModel();
            HtmlDocument doc1 = new HtmlDocument();
            doc1.LoadHtml(schoolNew.Content);
            HtmlNodeCollection imgs = doc1.DocumentNode.SelectNodes("//img");
            if (imgs != null)
            {
                foreach (HtmlNode img in imgs)
                {
                    img.Remove();
                }
            }
            DBContext db = new DBContext();
            model.Author = schoolNew.Author;
            model.Id = schoolNew.Id;
            model.Content = doc1.DocumentNode.InnerText.Trim();
            model.Title = schoolNew.Title;
            model.PublishTime = schoolNew.PublishTime.ToShortDateString();
            if (db.Images.Where(d => d.SchoolNewId == schoolNew.Id).FirstOrDefault()!=null)
            model.Images = db.Images.Where(d => d.SchoolNewId == schoolNew.Id).FirstOrDefault().Src;
            model.SchoolName = db.Schools.Find(schoolNew.SchoolId).Name;
            model.NewTypeName = db.NewTypes.Find(schoolNew.NewTypeId).Name;
            model.ViewNumber = schoolNew.ViewNumber;
            model.ThumbUp = schoolNew.ThumbUp;
            model.CommentNumber = db.Comments.Where(d => d.SchoolNewId == schoolNew.Id).Count();
            model.SchoolName = db.Schools.Find(schoolNew.SchoolId).Name;
            return model;
        }
        public static SchoolNewModel Transfer1(SchoolNew schoolNew)
        {
            SchoolNewModel model = new SchoolNewModel();
            DBContext db = new DBContext();
            model.Author = schoolNew.Author;
            model.Id = schoolNew.Id;
            model.Content = schoolNew.Content;
            model.Title = schoolNew.Title;
            model.PublishTime = schoolNew.PublishTime.ToShortDateString();
            if (db.Images.Where(d => d.SchoolNewId == schoolNew.Id).FirstOrDefault() != null)
                model.Images = db.Images.Where(d => d.SchoolNewId == schoolNew.Id).FirstOrDefault().Src;
            model.SchoolName = db.Schools.Find(schoolNew.SchoolId).Name;
            model.NewTypeName = db.NewTypes.Find(schoolNew.NewTypeId).Name;
            model.ViewNumber = schoolNew.ViewNumber;
            model.ThumbUp = schoolNew.ThumbUp;
            model.CommentNumber = db.Comments.Where(d => d.SchoolNewId == schoolNew.Id).Count();
            model.SchoolName = db.Schools.Find(schoolNew.SchoolId).Name;
            return model;
        }
    }
}