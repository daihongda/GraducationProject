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
using GraduationProject.Filters;
using GraduationProject.Common;
using GraduationProject.Models;

namespace GraduationProject.Controllers
{
    [CheckLogin]
    public class NewTypeUrlsController : Controller
    {
        private DBContext db = new DBContext();
        private NewTypeUrlBll bll = new NewTypeUrlBll();

        // GET: NewTypeUrls
        public ActionResult Index()
        {
            var list = ForeachClass.GetTableAttribute<NewTypeUrl>();
            @ViewBag.Attributes = new SelectList(list, "Name", "DisplayName");
            return View(db.NewTypeUrls.ToList());
        }
        public JsonResult GetNewTypeUrlList(int pageIndex = 1, int perPage = 10, int orderBy = 0, int orderType = 0)
        {
            var data = bll.GetByPage(pageIndex, perPage, orderBy, orderType).Select(d => new
            {
                NewTypeId = d.NewTypeId,
                SchoolId = d.SchoolId,
                Url = d.Url
            });
            string[] tableHeaders = ForeachClass.GetTableHeaders<NewTypeUrl>();

            return Json(new JsonModel("加载数据成功！", data, tableHeaders, bll.GetAll().Count));
        }
        public JsonResult Search(string searchContent, int pageIndex = 1, int perPage = 10, int orderBy = 0, int orderType = 0)
        {

            var data = bll.SearchProvince(searchContent, pageIndex, perPage, orderBy, orderType).Select(d => new
            {
                NewTypeId = d.NewTypeId,
                SchoolId = d.SchoolId,
                Url = d.Url
            });
            string[] tableHeaders = ForeachClass.GetTableHeaders<NewTypeUrl>();
            return Json(new JsonModel("", data, tableHeaders));
        }

        // GET: NewTypeUrls/Details/5
        public ActionResult Details(int? schoolId, int? newTypeId)
        {
            if (schoolId == null || newTypeId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewTypeUrl newTypeUrl = db.NewTypeUrls.Where(d => d.SchoolId == schoolId && d.NewTypeId == newTypeId).FirstOrDefault();
            if (newTypeUrl == null)
            {
                return HttpNotFound();
            }
            return View(newTypeUrl);
        }

        // GET: NewTypeUrls/Create
        public ActionResult Create()
        {
            ViewBag.SchoolId = new SelectList(db.Schools, "Id", "Name");
            ViewBag.NewTypeId = new SelectList(db.NewTypes, "Id", "Name");
            return View();
        }

        // POST: NewTypeUrls/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SchoolId,NewTypeId,Url,IsDelete")] NewTypeUrl newTypeUrl)
        {
            if (ModelState.IsValid)
            {
                db.NewTypeUrls.Add(newTypeUrl);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(newTypeUrl);
        }

        // GET: NewTypeUrls/Edit/5
        public ActionResult Edit(int? schoolId,int? newTypeId)
        {
            if (schoolId == null || newTypeId==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewTypeUrl newTypeUrl = db.NewTypeUrls.Where(d=>d.SchoolId == schoolId && d.NewTypeId == newTypeId).FirstOrDefault();
            if (newTypeUrl == null)
            {
                return HttpNotFound();
            }
            return View(newTypeUrl);
        }

        // POST: NewTypeUrls/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SchoolId,NewTypeId,Url,IsDelete")] NewTypeUrl newTypeUrl)
        {
            if (ModelState.IsValid)
            {
                db.Entry(newTypeUrl).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(newTypeUrl);
        }

        // GET: NewTypeUrls/Delete/5
        public ActionResult Delete(int? schoolId, int? newTypeId)
        {
            if (schoolId == null || newTypeId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewTypeUrl newTypeUrl = db.NewTypeUrls.Where(d => d.SchoolId == schoolId && d.NewTypeId == newTypeId).FirstOrDefault();
            if (newTypeUrl == null)
            {
                return HttpNotFound();
            }
            return View(newTypeUrl);
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

        // POST: NewTypeUrls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NewTypeUrl newTypeUrl = db.NewTypeUrls.Find(id);
            db.NewTypeUrls.Remove(newTypeUrl);
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
