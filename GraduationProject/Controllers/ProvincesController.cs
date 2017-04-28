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
    public class ProvincesController : Controller
    {
        private DBContext db = new DBContext();
        private ProvinceBll bll = new ProvinceBll();

        // GET: Provinces
        public ActionResult Index()
        {
            var list = ForeachClass.GetTableAttribute<Province>();
            
            @ViewBag.Attributes = new SelectList(list,"Name","DisplayName");
            return View(db.Provinces.ToList());
        }
        public JsonResult GetProvinceList(int pageIndex = 1, int perPage = 10, int orderBy = 0, int orderType = 0)
        {
            var data = bll.GetByPage(pageIndex, perPage, orderBy, orderType).Select(d => new
            {
                Id = d.Id,
                Name = d.Name
            });
            string[] tableHeaders = ForeachClass.GetTableHeaders<Province>();

            return Json(new JsonModel("加载数据成功！", data, tableHeaders, bll.GetAll().Count));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchContent"></param>
        /// <param name="pageIndex"></param>
        /// <param name="perPage"></param>
        /// <param name="orderBy"></param>
        /// <param name="orderType"></param>
        /// <returns></returns>
        public JsonResult Search(string searchContent, int pageIndex = 1, int perPage = 10, int orderBy = 0, int orderType = 0)
        {

            var data = bll.SearchProvince(searchContent, pageIndex, perPage, orderBy, orderType).Select(d => new
            {
                Id = d.Id,
                Name = d.Name
            });
            string[] tableHeaders = ForeachClass.GetTableHeaders<Province>();
            return Json(new JsonModel("", data, tableHeaders));
        }

        // GET: Provinces/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Province province = db.Provinces.Find(id);
            if (province == null)
            {
                return HttpNotFound();
            }
            return View(province);
        }

        // GET: Provinces/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Provinces/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] Province province)
        {
            if (ModelState.IsValid)
            {
                db.Provinces.Add(province);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(province);
        }

        // GET: Provinces/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Province province = db.Provinces.Find(id);
            if (province == null)
            {
                return HttpNotFound();
            }
            return View(province);
        }

        // POST: Provinces/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] Province province)
        {
            if (ModelState.IsValid)
            {
                db.Entry(province).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(province);
        }

        // GET: Provinces/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Province province = db.Provinces.Find(id);
            if (province == null)
            {
                return HttpNotFound();
            }
            return View(province);
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
        // POST: Provinces/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Province province = db.Provinces.Find(id);
            db.Provinces.Remove(province);
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
