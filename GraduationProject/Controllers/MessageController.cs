using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DHD.ENTITY;
using DHD.BLL;
using GraduationProject.Common;
using GraduationProject.Models;

namespace GraduationProject.Controllers
{
    public class MessageController : Controller
    {
        private MessageBll bll = new MessageBll();
        private DBContext db = new DBContext();
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
                Message msg = new Message();
                msg.Content = message.Content;
                msg.CreatedTime = DateTime.Now;
                msg.From = GeneralCommon.getCurrentUserId();
                msg.Title = message.Title;
                //msg.To = 1;
                db.Messages.Add(msg);
                db.SaveChanges();
                return RedirectToAction("Message", "RequestList");
            }
            return View(message);
        }
        public JsonResult GetMessgaeList(string searchContent="",int pageIndex=1,int perPage=10)
        {
            var list = bll.SearchMessage(searchContent, pageIndex, perPage);
            var messages = new List<MessageModel>();
            foreach (var message in list)
            {
                var item = new MessageModel();
                item.Title = message.Title;
                item.Content = message.Content;
                item.Id = message.Id;
                item.CreatedTime = message.CreatedTime.ToShortDateString();
                item.From = message.From;
                item.FromName = db.Users.Find(message.From) == null ? "dhd" : db.Users.Find(message.From).NickName;
                item.HavaExamined = message.HaveExamined;
                messages.Add(item);
            }
            int count = bll.SearchMessages(searchContent).Count();
            PageModel model = new PageModel();
            model.CurrentPage = pageIndex;
            model.Data = messages;
            if (count % perPage == 0)
            {
                model.PageCount = count / perPage;
            }
            else
            {
                model.PageCount = count / perPage + 1;
            }
            return Json(new JsonModel("1", model));
        }
        public JsonResult ExamineRequest(int id)
        {
            var message = db.Messages.Find(id);
            if (message == null)
            {
                return Json(new JsonModel("审核的申请的不存在！"));
            }
            else
            {
                message.HaveExamined = 1;
                db.SaveChanges();
                return Json(new JsonModel("审核成功！", null));
            }
        }
        public ActionResult RequestList()
        {
            return View();
        }
    }
}