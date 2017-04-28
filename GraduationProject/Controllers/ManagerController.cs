using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DHD.ENTITY;
using DHD.BLL;
using GraduationProject.Models;
using GraduationProject.Filters;
namespace GraduationProject.Controllers
{
    public class ManagerController : Controller
    {
        // GET: Manager
        ManagerBll bll = new ManagerBll();
        public ActionResult Register()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult EnterForBack()
        {
            return View();
        }
        public ActionResult LoginValidate(BackLoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                var manager = bll.GetAll().Where(d => d.UserName == loginModel.UserName).FirstOrDefault();
                if (manager != null)
                {
                    if (manager.Password == loginModel.Password)
                    {
                        HttpCookie cookie = new HttpCookie("managerId");
                        cookie.Value = manager.Id.ToString();
                        HttpContext.Response.Cookies.Add(cookie);

                        return RedirectToAction("Index", "Schools");
                    }
                    else
                    {
                        ModelState.AddModelError("", "密码错误！");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "用户名不存在！");
                }
            }
            return View("Login",loginModel);
        }
        public ActionResult RegisterValidate(BackRegisterModel registerModel)
        {
            if (ModelState.IsValid)
            {
                var manager = bll.GetAll().Where(d => d.UserName == registerModel.UserName).FirstOrDefault();
                if (manager == null)
                {
                    var newManager = new Manager();
                    newManager.UserName = registerModel.UserName;
                    newManager.NickName = registerModel.NickName;
                    newManager.Password = registerModel.Password;
                    manager = bll.Add(newManager);
                    HttpCookie cookie = new HttpCookie("managerId");
                    cookie.Value = manager.Id.ToString();
                    HttpContext.Response.Cookies.Add(cookie);
                    return RedirectToAction("Index", "Schools");
                }
                else
                {
                    ModelState.AddModelError("", "用户名已存在！");
                }
            }
            // If we got this far, something failed, redisplay form
            return View("Register",registerModel);
        }
        public ActionResult LogOut()
        {
            return View("");
        }
    }
}