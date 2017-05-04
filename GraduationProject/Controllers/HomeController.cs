using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DHD.BLL;
using GraduationProject.Models;
using DHD.ENTITY;
using GraduationProject.Common;
using GraduationProject.Filters;
namespace GraduationProject.Controllers
{
    public class HomeController : Controller
    {
        DBContext db = new DBContext();
        // GET: Home

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SchoolNews(int schoolNewId=-1,int pageIndex=1)
        {
            PagerModel model = new PagerModel();
            model.SchoolId = schoolNewId;
            model.CurrentPage = pageIndex;
            return View(model);
        }
        public ActionResult Schools()
        {
            return View();
        }
        /// <summary>
        /// 获取对应的学校新闻的详情
        /// </summary>
        /// <param name="schoolNewId"></param>
        /// <returns></returns>
        public ActionResult Detail(int schoolNewId = 473)
        {
            var schoolNew = db.SchoolNews.Find(schoolNewId);

            return View(SchoolNewModel.Transfer1(schoolNew));
        }
        [CheckedLogin]
        public ActionResult UserSetting()
        {
            var userId = GeneralCommon.getCurrentUserId();
            var user = db.Users.Find(userId);
            if (user != null)
            {
                return View(user);
            }
            else
            {
                return View();
            }
        }
        #region 用户相关接口
        public JsonResult SettingPassword(string password,string confirmPassword)
        {
            var id = GeneralCommon.getCurrentUserId();
            var user = db.Users.Find(id);
            if (password == confirmPassword&&user!=null)
            {
                user.Password = password;
                db.SaveChanges();
                return Json(new JsonModel("", null));
            }
            return Json(new JsonModel("当前用户登录异常"));
        }
        #endregion
        #region 新闻相关接口
        public JsonResult LoadSchoolListHot()
        {
            var data = from p in db.SchoolNews
                       group p by p.SchoolId into g
                       select new
                       {
                           Id = g.Key,
                           Name = db.Schools.Where(s => s.Id == g.Key).FirstOrDefault().Name,
                           TotalView = g.Sum(p => p.ViewNumber),
                           Url = db.Schools.Where(s => s.Id == g.Key).FirstOrDefault().SchoolIconUrl
                       };
            var list = data.OrderBy(d => d.TotalView).Take(10).ToList();
            return Json(new JsonModel("获取成功", list), JsonRequestBehavior.AllowGet);
        }
        public JsonResult LoadSchoolNew(int schoolNewId)
        {
            var schoolNew = db.SchoolNews.Find(schoolNewId);
            var data = SchoolNewModel.Transfer(schoolNew);
            return Json(new JsonModel("", data));
        }
        public JsonResult LoadSchoolNews(int newType=-1, int timespan=-1, int haveImage=-1, int pageIndex = 1, int perPage = 10, int orderBy = 0, int orderType = 0, int schoolId = -1)
        {
            //先显示有图片的
            var data = db.SchoolNews.AsQueryable();
            if (newType != -1)
            {
                data = data.Where(d=>d.NewTypeId == newType);
            }
            if (haveImage != -1)
            {
                data = data.Where(d => d.HaveImg == haveImage);
            }
            if (timespan != -1)
            {
                var t = DateTime.Now.AddDays(-timespan);
                data = data.Where(d => d.PublishTime >= t);
            }
            var schoolNews = data.ToList();
            if (schoolId != -1)
            {
                data = data.Where(d => d.SchoolId == schoolId);
            }
            var count = data.Count();
            data = data.OrderBy(d => d.Id).Skip((pageIndex - 1) * perPage).Take(perPage);


            List<SchoolNewModel> list = new List<SchoolNewModel>();
            foreach (var item in data)
            {
                var schoolNew = SchoolNewModel.Transfer(item);
                list.Add(schoolNew);
            }
            var pager = new PageModel();
            pager.CurrentPage = pageIndex;
            if (count % perPage == 0)
            {
                pager.PageCount = count / perPage;
            }
            else
            {
                pager.PageCount = count / perPage + 1;
            }
            pager.Data = list;
            return Json(new JsonModel("加载数据成功！", pager));
        }
        public JsonResult LoadSchoolNewsHot(int number = 10)
        {
            var data = db.SchoolNews.Where(d => d.HaveImg == 1).OrderBy(d => d.ViewNumber).Take(number);
            List<SchoolNewModel> list = new List<SchoolNewModel>();
            foreach (var item in data)
            {
                var schoolNew = SchoolNewModel.Transfer(item);
                list.Add(schoolNew);
            }
            return Json(new JsonModel("获取成功", list), JsonRequestBehavior.AllowGet);
        }
        public JsonResult LoadSchoolNewsRecent(int number = 10)
        {
            var data = db.SchoolNews.Where(d => d.HaveImg == 1).OrderByDescending(d => d.PublishTime).Take(number);
            List<SchoolNewModel> list = new List<SchoolNewModel>();
            foreach (var item in data)
            {
                var schoolNew = SchoolNewModel.Transfer(item);
                list.Add(schoolNew);
            }
            return Json(new JsonModel("获取成功", list), JsonRequestBehavior.AllowGet);
        }
        public JsonResult ThumbUpSchoolNew(int schoolNewId)
        {
            try
            {
                //获取当前登录用户的userid
                int userId = 1;
                UserThumb t = db.UserThumbs.Where(d => d.SchoolNewId == schoolNewId && d.UserId == userId && d.Type == 0).FirstOrDefault();
                if (t != null)
                {
                    return Json(new JsonModel("您已经赞过这篇文章了。"));
                }

                SchoolNew schoolNew = db.SchoolNews.Find(schoolNewId);
                schoolNew.ThumbUp = schoolNew.ThumbUp+1;
               
                UserThumb thumb = new UserThumb();
                thumb.UserId = userId;
                thumb.SchoolNewId = schoolNewId;
                thumb.Type = 0;
                thumb.OperateTime = DateTime.Now;
                db.UserThumbs.Add(thumb);
                db.SaveChanges();
                return Json(new JsonModel("点赞成功！", SchoolNewModel.Transfer(schoolNew)));
            }
            catch(Exception ex)
            {
                return Json(new JsonModel(ex.ToString()));
            }
        }

        
        #endregion
        #region 评论相关接口
        public JsonResult GetComments(int schoolNewId,int page=1,int perpage=5)
        {
            var comments = db.Comments.Where(d => d.SchoolNewId == schoolNewId & d.IsDelete == 0).OrderByDescending(d=>d.PublishTime).Skip((page-1)*perpage).Take(perpage);
            List<CommentModel> commentmodels = new List<CommentModel>();
            foreach(var comment in comments){
                commentmodels.Add(CommentModel.Transfer(comment));
            }

            return Json(new JsonModel("获取评论成功", commentmodels));
        }
        public JsonResult PublishComment(Comment comment)
        {
            comment.PublishTime = DateTime.Now;
            db.Comments.Add(comment);
            db.SaveChanges();
            return Json(new JsonModel("评论成功", CommentModel.Transfer(comment)));
        }
        public JsonResult ThumbUpComment(int commentId)
        {
            try
            {
                ///之后写个获取当前登录用户id的公共方法
                int userId = 1;
                UserThumb ut = db.UserThumbs.Where(d => d.UserId == userId && d.SchoolNewId == commentId && d.Type == 1).FirstOrDefault();
                if (ut != null)
                {

                    Comment comment = db.Comments.Find(commentId);
                    comment.ThumbUp = comment.ThumbUp - 1;
                    db.UserThumbs.Remove(ut);
                    db.SaveChanges();
                    return Json(new JsonModel("取消赞成功！", comment));
                }
                else
                {
                    Comment comment = db.Comments.Find(commentId);
                    comment.ThumbUp = comment.ThumbUp + 1;

                    UserThumb thumb = new UserThumb();
                    thumb.UserId = userId;
                    thumb.SchoolNewId = commentId;
                    thumb.Type = 1;
                    thumb.OperateTime = DateTime.Now;

                    db.UserThumbs.Add(thumb);
                    db.SaveChanges();

                    return Json(new JsonModel("点赞成功！", comment));
                }
            }
            catch (Exception ex)
            {
                return Json(new JsonModel("发生异常:"+ex.ToString()));
            }
        }
        public JsonResult DeleteComment(int commentId)
        {
            var comment = db.Comments.Find(commentId);
            if (comment != null)
            {
                comment.IsDelete = 1;
                db.SaveChanges();
                return Json(new JsonModel("删除成功", null));
            }
            return Json(new JsonModel("删除失败"));
            
        }
        #endregion
        public ActionResult Register()
        {
            return View();
        }
        public ActionResult RegisterValidate(UserRegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var user = db.Users.Where(d => d.UserName == model.Username).FirstOrDefault();
                if (user != null) {
                    ModelState.AddModelError("","用户名已存在!");
                    return View("Register", model);

                }
                user = db.Users.Where(d=>d.NickName == model.NickName).FirstOrDefault();
                if (user != null)
                {
                    ModelState.AddModelError("", "昵称已存在!");
                    return View("Register", model);
                }
                User userNew = new User();
                userNew.Email = model.Email;
                userNew.NickName = model.NickName;
                userNew.Password = model.Password;
                userNew.PhoneNumber = model.PhoneNumber;
                userNew.UserName = model.Username;
                userNew.SchoolId = model.SchoolId;
                db.Users.Add(userNew);
                db.SaveChanges();

                HttpCookie cookie = new HttpCookie("userId");
                cookie.Value = userNew.Id.ToString();
                cookie.Expires = DateTime.Now.AddHours(2);
                HttpContext.Response.Cookies.Add(cookie);

                return RedirectToAction("Index", "Home");
            }
            return View("Register",model);
        }
        public ActionResult Login()
        {
            return View();
        
        }
        public ActionResult LoginValidate(UserLoginModel model)
        {
            if (ModelState.IsValid)
            {
                var user = db.Users.Where(d=>d.UserName == model.Username).FirstOrDefault();
                if (user == null)
                {
                    ModelState.AddModelError("", "用户名不存在！");
                    return View("Login", model);
                }
                if (user.Password != model.Password)
                {
                    ModelState.AddModelError("", "密码错误！");
                    return View("Login", model);
                }
                HttpCookie cookie = new HttpCookie("userId");
                cookie.Value = user.Id.ToString();
                cookie.Expires = DateTime.Now.AddHours(2);
                HttpContext.Response.Cookies.Add(cookie);
                return RedirectToAction("Index","Home");
            }
            return View("Login", model);
        }
        public ActionResult QuickLogin()
        {
            return View();
        }
        public ActionResult QuickLoginValidate(UserQuickLoginModel model)
        {
            if (ModelState.IsValid)
            {
                var user = db.Users.Where(d => d.PhoneNumber == model.PhoneNumber).FirstOrDefault();
                if (user == null)
                {
                    ModelState.AddModelError("", "该手机号目前尚未注册！");
                    return View("QuickLogin", model);
                }
                if (model.IdentifyingCode != GeneralCommon.GetValidateCode())
                {
                    ModelState.AddModelError("", "验证码错误！");
                    return View("QuickLogin", model);
                }
                HttpCookie cookie = new HttpCookie("userId");
                cookie.Value = user.Id.ToString();
                cookie.Expires = DateTime.Now.AddHours(2);
                Response.Cookies.Add(cookie);
                return RedirectToAction("Index", "Home");
            }
            return View("QuickLogin", model);
        }
        public ActionResult Test()
        {
            return View();
        }
        public ActionResult LogOut()
        {
            HttpCookie cookie = HttpContext.Request.Cookies["userId"];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddDays(-1);
                //HttpContext.Request.Cookies.Remove("userId");
            }
            return View("Login");
        }
        public JsonResult GetMultiSelect(int currentPage,int provinceId=-1,int areaId=-1,int perPage = 10)
        {
            var provinces = db.Provinces.AsQueryable();
            var areas = db.Areas.AsQueryable();
            var schools = db.Schools.AsQueryable();
            if (provinceId != -1)
            {
                areas = areas.Where(d => d.ProvinceId == provinceId);
            }
            if (areaId != -1)
            {
                schools = schools.Where(d => d.AreaId == areaId);
            }
            //
            var model = new MultiSelectModel();
            model.ProvinceId = provinceId;
            model.Provinces = provinces.ToList();
            model.AreaId = areaId;
            model.Areas = areas.ToList();
            model.Schools = schools.OrderBy(d=>d.Id).Skip((currentPage - 1) * perPage).Take(perPage).ToList();
            var count = schools.Count();
            if (count % perPage == 0)
            {
                model.PageCount = count / perPage;
            }
            else
            {
                model.PageCount = count / perPage + 1;
            }
            model.CurrentPage = currentPage;
            return Json(new JsonModel("", model));
        }
        public ActionResult GetSchoolNewTypes(int schoolId=-1)
        {
            var typeUrls = db.NewTypeUrls.AsQueryable();
            if (schoolId != -1)
            {
                typeUrls = typeUrls.Where(d => d.SchoolId == schoolId);
                var types = new List<NewType>();
                foreach (var item in typeUrls)
                {
                    var type = db.NewTypes.Find(item.NewTypeId);
                    types.Add(type);
                }
                return Json(new JsonModel("", types));
            }
            else
            {
                var types = db.NewTypes.ToList();
                return Json(new JsonModel("", types));
            }
            
            
        }
        

    }
}