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
    public class NewTypesController : Controller
    {
        private DBContext db = new DBContext();
        private NewTypeBll bll = new NewTypeBll();
        // GET: NewTypes
        public ActionResult Index()
        {
            var list = ForeachClass.GetTableAttribute<NewType>();
            @ViewBag.Attributes = new SelectList(list, "Name", "DisplayName");
            return View(db.NewTypes.ToList());
        }
        public JsonResult GetNewTypeList(int pageIndex = 1, int perPage = 10, int orderBy = 0, int orderType = 0)
        {
            var data = bll.GetByPage(pageIndex, perPage, orderBy, orderType).Select(d => new
            {
                Id = d.Id,
                Name = d.Name,
            });
            string[] tableHeaders = ForeachClass.GetTableHeaders<NewType>();

            return Json(new JsonModel("加载数据成功！", data, tableHeaders, bll.GetAll().Count));
        }
        public JsonResult Search(string searchContent, int pageIndex = 1, int perPage = 10, int orderBy = 0, int orderType = 0)
        {

            var data = bll.SearchProvince(searchContent, pageIndex, perPage, orderBy, orderType).Select(d => new
            {
                Id = d.Id,
                Name = d.Name,
            });
            string[] tableHeaders = ForeachClass.GetTableHeaders<NewType>();
            return Json(new JsonModel("", data, tableHeaders));
        }

        // GET: NewTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewType newType = db.NewTypes.Find(id);
            if (newType == null)
            {
                return HttpNotFound();
            }
            return View(newType);
        }

        // GET: NewTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NewTypes/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,IsDelete")] NewType newType)
        {
            if (ModelState.IsValid)
            {
                db.NewTypes.Add(newType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(newType);
        }

        // GET: NewTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewType newType = db.NewTypes.Find(id);
            if (newType == null)
            {
                return HttpNotFound();
            }
            return View(newType);
        }

        // POST: NewTypes/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,IsDelete")] NewType newType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(newType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(newType);
        }

        // GET: NewTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewType newType = db.NewTypes.Find(id);
            if (newType == null)
            {
                return HttpNotFound();
            }
            return View(newType);
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
        // POST: NewTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NewType newType = db.NewTypes.Find(id);
            db.NewTypes.Remove(newType);
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
