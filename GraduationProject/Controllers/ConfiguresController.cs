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
using GraduationProject.Common;
using GraduationProject.Models;
using GraduationProject.Filters;
namespace GraduationProject.Controllers
{
    [CheckLogin]
    public class ConfiguresController : Controller
    {
        private DBContext db = new DBContext();
        private ConfigureBll bll = new ConfigureBll();
        // GET: Configures
        public ActionResult Index()
        {
            var list = ForeachClass.GetTableAttribute<Configure>();
            @ViewBag.Attributes = new SelectList(list, "Name", "DisplayName");
            return View(db.Configures.ToList());
        }
        /// <summary>
        /// 根据页码，每页的数据条数
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="perPage"></param>
        /// <param name="orderBy"></param>
        /// <param name="orderType"></param>
        /// <returns></returns>
        public JsonResult GetConfigureList(int pageIndex = 1, int perPage = 10, int orderBy = 0, int orderType = 0)
        {
            var data = bll.GetByPage(pageIndex, perPage, orderBy, orderType).Select(d => new
            {
                Id = d.Id,
                ListPath = d.ListPath,
                HrefPath = d.HrefPath,
                TitlePath = d.TitlePath,
                TimePath = d.TimePath,
                ContentPath = d.ContentPath,
                AuthorPath = d.AuthorPath,
                SchoolName = db.Schools.Where(s => s.Id == d.SchoolId).FirstOrDefault().Name,
                NextPageModule = d.NextPageModule
            });
            string[] tableHeaders = ForeachClass.GetTableHeaders<Configure>();

            return Json(new JsonModel("加载数据成功！", data, tableHeaders, bll.GetAll().Count));
        }
        public JsonResult Search(string searchContent, int pageIndex = 1, int perPage = 10, int orderBy = 0, int orderType = 0)
        {

            var data = bll.SearchProvince(searchContent, pageIndex, perPage, orderBy, orderType).Select(d => new
            {
                Id = d.Id,
                ListPath = d.ListPath,
                HrefPath = d.HrefPath,
                TitlePath = d.TitlePath,
                TimePath = d.TimePath,
                ContentPath = d.ContentPath,
                AuthorPath = d.AuthorPath,
                SchoolName = db.Schools.Where(s => s.Id == d.SchoolId).FirstOrDefault().Name,
                NextPageModule = d.NextPageModule
            });
            string[] tableHeaders = ForeachClass.GetTableHeaders<Configure>();
            return Json(new JsonModel("", data, tableHeaders));
        }
        // GET: Configures/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Configure configure = db.Configures.Find(id);
            if (configure == null)
            {
                return HttpNotFound();
            }
            return View(configure);
        }

        // GET: Configures/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Configures/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ListPath,HrefPath,TitlePath,TimePath,ContentPath,SchoolId,IsDelete,AuthorPath,NextPageModule")] Configure configure)
        {
            if (ModelState.IsValid)
            {
                db.Configures.Add(configure);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(configure);
        }

        // GET: Configures/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Configure configure = db.Configures.Find(id);
            if (configure == null)
            {
                return HttpNotFound();
            }
            return View(configure);
        }

        // POST: Configures/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ListPath,HrefPath,TitlePath,TimePath,ContentPath,SchoolId,IsDelete,AuthorPath,NextPageModule")] Configure configure)
        {
            if (ModelState.IsValid)
            {
                db.Entry(configure).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(configure);
        }

        // GET: Configures/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Configure configure = db.Configures.Find(id);
            if (configure == null)
            {
                return HttpNotFound();
            }
            return View(configure);
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
        // POST: Configures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Configure configure = db.Configures.Find(id);
            db.Configures.Remove(configure);
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
