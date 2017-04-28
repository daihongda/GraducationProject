using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DHD.ENTITY;
using DHD.BLL;
using GraduationProject.Models;
using GraduationProject.Filters;
using GraduationProject.Common;

namespace GraduationProject.Controllers
{
    [CheckLogin]
    public class SchoolNewsController : Controller
    {
        private DBContext db = new DBContext();
        SchoolNewBll bll = new SchoolNewBll();

        // GET: SchoolNews
        public ActionResult Index()
        {
            var attributes = ForeachClass.GetTableAttribute<SchoolNew>();
            @ViewBag.Attributes = new SelectList(attributes, "Name", "DisplayName");
            return View(db.SchoolNews.ToList());
        }
        /// <summary>
        /// 根据页码，每页的数据条数
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="perPage"></param>
        /// <param name="orderBy"></param>
        /// <param name="orderType"></param>
        /// <returns></returns>
        public JsonResult GetSchoolNewList(int pageIndex = 1, int perPage = 10, int orderBy = 0, int orderType = 0)
        {
            var data = bll.GetByPage(pageIndex, perPage, orderBy, orderType).Select(d => new
            {
                Id = d.Id,
                Title = d.Title,
                Content = d.Content,
                PublishTime = d.PublishTime.ToShortDateString(),
                Author = d.Author
            });
            string[] tableHeaders = ForeachClass.GetTableHeaders<SchoolNew>();

            return Json(new JsonModel("加载数据成功！", data, tableHeaders, bll.GetAll().Count));
        }
        public JsonResult Search(string searchContent, int pageIndex = 1, int perPage = 10, int orderBy = 0, int orderType = 0)
        {

            var data = bll.SearchSchoolNew(searchContent, pageIndex, perPage, orderBy, orderType).Select(d => new
            {
                Id = d.Id,
                Title = d.Title,
                Content = d.Content,
                PublishTime = d.PublishTime.ToShortDateString(),
                Author = d.Author
            });
            string[] tableHeaders = ForeachClass.GetTableHeaders<SchoolNew>();
            return Json(new JsonModel("", data, tableHeaders));
        }
        // GET: SchoolNews/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SchoolNew schoolNew = db.SchoolNews.Find(id);
            if (schoolNew == null)
            {
                return HttpNotFound();
            }
            return View(schoolNew);
        }

        // GET: SchoolNews/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SchoolNews/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Content,PublishTime,Author,IsDelete")] SchoolNew schoolNew)
        {
            if (ModelState.IsValid)
            {
                db.SchoolNews.Add(schoolNew);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(schoolNew);
        }

        // GET: SchoolNews/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SchoolNew schoolNew = db.SchoolNews.Find(id);
            if (schoolNew == null)
            {
                return HttpNotFound();
            }
            return View(schoolNew);
        }

        // POST: SchoolNews/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Content,PublishTime,Author,IsDelete")] SchoolNew schoolNew)
        {
            if (ModelState.IsValid)
            {
                db.Entry(schoolNew).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(schoolNew);
        }

        // GET: SchoolNews/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SchoolNew schoolNew = db.SchoolNews.Find(id);
            if (schoolNew == null)
            {
                return HttpNotFound();
            }
            return View(schoolNew);
        }
        public JsonResult DeleteRange(string ids)
        {
            try
            {
                if (ids == "" || ids == null)
                {
                    return Json(new JsonModel("删除对象不可以为空或者空字符串！"));
                }
                if (bll.DeleteRange(ids) > 0)
                {
                    return Json(new JsonModel("批量删除成功！", null));
                }
            }
            catch (Exception ex)
            {
                return Json(new JsonModel("批量删除失败！"));
            }
            return Json(new JsonModel("批量删除失败！"));
        }

        // POST: SchoolNews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SchoolNew schoolNew = db.SchoolNews.Find(id);
            db.SchoolNews.Remove(schoolNew);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
       
    }
}
