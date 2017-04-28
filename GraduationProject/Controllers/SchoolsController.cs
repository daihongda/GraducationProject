using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DHD.ENTITY;
using System.Reflection;
using System.ComponentModel;
using GraduationProject.Models;
using DHD.BLL;
using GraduationProject.Common;
using GraduationProject.Filters;
namespace GraduationProject.Controllers
{
    [CheckLogin]
    public class SchoolsController : Controller
    {
        private DBContext db = new DBContext();
        private SchoolBll bll = new SchoolBll();
        // GET: Schools
        public ActionResult Index()
        {
            var attributes = ForeachClass.GetTableAttribute<School>();
            @ViewBag.Attributes = new SelectList(attributes, "Name", "DisplayName");
            return View();
        }
        public ActionResult Catch()
        {
            @ViewBag.SchoolId = new SelectList(db.Schools, "Id", "Name");

            return View();
        }
        /// <summary>
        /// 根据页码，每页的数据条数
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="perPage"></param>
        /// <param name="orderBy"></param>
        /// <param name="orderType"></param>
        /// <returns></returns>
        public JsonResult GetSchoolList(int pageIndex = 1, int perPage = 10, int orderBy = 0, int orderType = 0)
        {
            var data = bll.GetByPage(pageIndex, perPage, orderBy, orderType).Select(d => new{ 
                  Id = d.Id,
                  Name = d.Name,
                  Introduce = d.Introduce,
                  HomePage = d.HomePage
            });
            string[] tableHeaders = ForeachClass.GetTableHeaders<School>();

            return Json(new JsonModel("加载数据成功！", data, tableHeaders,bll.GetAll().Count));
        }
        public JsonResult Search(string searchContent, int pageIndex = 1, int perPage = 10, int orderBy = 0, int orderType = 0)
        {

            var data = bll.SearchSchool(searchContent, pageIndex, perPage, orderBy, orderType).Select(d => new
            {
                Id = d.Id,
                Name = d.Name,
                Introduce = d.Introduce,
                HomePage = d.HomePage
            }); ; 
            string[] tableHeaders = ForeachClass.GetTableHeaders<School>();
            return Json(new JsonModel("", data, tableHeaders));
        }

        // GET: Schools/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            School school = db.Schools.Find(id);
            if (school == null)
            {
                return HttpNotFound();
            }
            return View(school);
        }

        // GET: Schools/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Schools/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Introduce,HomePage,IsDelete")] School school)
        {
            if (ModelState.IsValid)
            {
                db.Schools.Add(school);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(school);
        }

        // GET: Schools/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            School school = db.Schools.Find(id);
            if (school == null)
            {
                return HttpNotFound();
            }
            return View(school);
        }

        // POST: Schools/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Introduce,HomePage,IsDelete")] School school)
        {
            if (ModelState.IsValid)
            {
                db.Entry(school).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(school);
        }

        // GET: Schools/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            School school = db.Schools.Find(id);
            if (school == null)
            {
                return HttpNotFound();
            }
            return View(school);
        }

        // POST: Schools/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            School school = db.Schools.Find(id);
            db.Schools.Remove(school);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public JsonResult DeleteRange(string ids)
        {
            try
            {
                if (ids == "" || ids == null)
                {
                    return Json(new JsonModel("删除对象不可以为空或者空字符串！"));
                }
                if(bll.DeleteRange(ids)>0){
                    return Json(new JsonModel("批量删除成功！",null));
                }
            }
            catch(Exception ex){
                return Json(new JsonModel("批量删除失败！"));
            }
            return Json(new JsonModel("批量删除失败！"));
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public JsonResult GetNewType(int schoolId)
        {
            var school = db.Schools.Find(schoolId);
            var newTypes = db.NewTypeUrls.Where(d => d.SchoolId == schoolId).Select(d => new { 
                Name = db.NewTypes.Where(t=>t.Id == d.NewTypeId).FirstOrDefault().Name,
                Url = d.Url,
                Id = d.NewTypeId
            });
            return Json(newTypes, JsonRequestBehavior.AllowGet);
        }

    }
}
