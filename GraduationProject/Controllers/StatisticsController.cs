using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DHD.ENTITY;
using GraduationProject.Models;

namespace GraduationProject.Controllers
{
    public class StatisticsController : Controller
    {
        // GET: Statistics
        DBContext db = new DBContext();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ReportBySchool()
        {
            var provinces = db.Provinces.ToList();
            var province = new Province();
            province.Id = -1;
            province.Name = "请选择省份";
            provinces.Add(province);
            provinces.Reverse();
            @ViewBag.Provinces = new SelectList(provinces,"Id","Name",-1);
            return View();
        }
        public ActionResult ReportByNewType()
        {
            return View();
        }
        public JsonResult GetAreas(int provinceId)
        {
            var areas = db.Areas.Where(d => d.ProvinceId == provinceId);
            return Json(new JsonModel("",areas));
        }
        public JsonResult GetChartData(int areaId,string timeSpan)
        {
            var schools = db.Schools.Where(d => d.AreaId == areaId);
            List<StatisticsModel> models = new List<StatisticsModel>();
            foreach (var school in schools)
            {
                //数据多了的话还是在schoolnew表中加一个commentNumber字段吧，现在这样，数据库的访问次数有点夸张- -
                var schoolnews = db.SchoolNews.Where(d => d.SchoolId == school.Id).Select(d => new
                { 
                    ViewNumber = d.ViewNumber,
                    CommentNumber = db.Comments.Where(c=>c.SchoolNewId == d.Id).Count()
                });
                StatisticsModel model = new StatisticsModel();
                model.y1 = schoolnews.Count();
                if (schoolnews != null && schoolnews.Count()>0)
                {
                    model.y2 = schoolnews.Sum(d => d.ViewNumber);
                    model.y3 = schoolnews.Sum(d => d.CommentNumber);
                }
                model.x1 = school.Name;
                models.Add(model);
            }
            
            return Json(new JsonModel("",models));
        }
        
    }
}