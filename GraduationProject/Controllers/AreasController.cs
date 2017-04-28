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
    public class AreasController : Controller
    {
        private DBContext db = new DBContext();
        private AreaBll bll = new AreaBll();
        // GET: Areas
        public ActionResult Index()
        {
            var areas = db.Areas.Include(a => a.Province);
            return View(areas.ToList());
        }

        /// <summary>
        /// 根据页码，每页的数据条数
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="perPage"></param>
        /// <param name="orderBy"></param>
        /// <param name="orderType"></param>
        /// <returns></returns>
        public JsonResult GetAreaList(int pageIndex = 1, int perPage = 10, int orderBy = 0, int orderType = 0)
        {
            var data = bll.GetByPage(pageIndex, perPage, orderBy, orderType).Select(d => new
            {
                Id = d.Id,
                Name = d.Name,
            });
            string[] tableHeaders = ForeachClass.GetTableHeaders<Area>();

            return Json(new JsonModel("加载数据成功！", data, tableHeaders, bll.GetAll().Count));
        }
        // GET: Areas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Area area = db.Areas.Find(id);
            if (area == null)
            {
                return HttpNotFound();
            }
            return View(area);
        }

        // GET: Areas/Create
        public ActionResult Create()
        {
            ViewBag.ProvinceId = new SelectList(db.Provinces, "Id", "Name");
            return View();
        }

        // POST: Areas/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,ProvinceId")] Area area)
        {
            if (ModelState.IsValid)
            {
                db.Areas.Add(area);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProvinceId = new SelectList(db.Provinces, "Id", "Name", area.ProvinceId);
            return View(area);
        }

        // GET: Areas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Area area = db.Areas.Find(id);
            if (area == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProvinceId = new SelectList(db.Provinces, "Id", "Name", area.ProvinceId);
            return View(area);
        }

        // POST: Areas/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,ProvinceId")] Area area)
        {
            if (ModelState.IsValid)
            {
                db.Entry(area).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProvinceId = new SelectList(db.Provinces, "Id", "Name", area.ProvinceId);
            return View(area);
        }

        // GET: Areas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Area area = db.Areas.Find(id);
            if (area == null)
            {
                return HttpNotFound();
            }
            return View(area);
        }

        // POST: Areas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Area area = db.Areas.Find(id);
            db.Areas.Remove(area);
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
