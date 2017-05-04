using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DHD.ENTITY;
using DHD.BLL;

namespace GraduationProject.Common
{
    public class GeneralCommon
    {
        static DBContext db = new DBContext();
        public static Manager getCurrentManager()
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies["managerId"];
            if (cookie != null)
            {
                int managerId = int.Parse(cookie.Value);
                ManagerBll bll = new ManagerBll();
                Manager manager = bll.Get(managerId);

                return manager;
            }
            else
            {
                //估计要报错，回头再看这里
                return null;
            }
        }
        public static int getCurrentUserId()
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies["userId"];
            if (cookie != null)
            {
                int userId = int.Parse(cookie.Value);
                var user = db.Users.Find(userId);
                if (user != null)
                    return userId;
                else
                    return 0;
            }
            else
            {
                return 0;
            }
        }
        public static string getCurrentUserName()
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies["userId"];
            if (cookie != null)
            {
                int userId = int.Parse(cookie.Value);
                User user = db.Users.Find(userId);
                return user.UserName;
            }
            return null;
        }
        public static string GetValidateCode()
        {
            int i = 0;
            string code = "";

            for (i = 0; i < 6; i++)
            {
                Random rd = new Random(DateTime.Now.Millisecond + i * 100);
                int s = rd.Next(10);
                code += s;
            }
            HttpCookie cookie = HttpContext.Current.Request.Cookies["IdentifyingCode"];
            if (cookie == null)
            {
                cookie = new HttpCookie("IdentifyingCode");
                cookie.Value = code;
                cookie.Expires = DateTime.Now.AddSeconds(60);
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
            else if (cookie.Value == "" || cookie.Value == null)
            {
                cookie.Value = code;
                cookie.Expires = DateTime.Now.AddSeconds(60);
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
            return cookie.Value;
        }
    }
}