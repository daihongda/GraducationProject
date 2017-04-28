using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DHD.ENTITY;
using DHD.BLL;
using GraduationProject.Models;

namespace GraduationProject.Controllers
{
    public class MessageController : Controller
    {
        private MessageBll bll = new MessageBll();
        // GET: Message
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SendMessage()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SendMessage(MessageModel message)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(message);
        }
        public JsonResult GetMessgaeList(string searchContent="",int pageIndex=1,int perPage=10)
        {
            var list = bll.SearchMessage(searchContent, pageIndex, perPage);
            return Json(new JsonModel("1",list));
        }
        public ActionResult RequestList()
        {
            return View();
        }
    }
}