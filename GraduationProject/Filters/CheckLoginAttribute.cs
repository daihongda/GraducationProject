using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GraduationProject.Common;
using DHD.ENTITY;
namespace GraduationProject.Filters
{
    public class CheckLoginAttribute:ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            Manager user = GeneralCommon.getCurrentManager();
            if (user == null)
            {
                filterContext.Result = new RedirectResult("/Manager/Login");
            }
        }
    }
}