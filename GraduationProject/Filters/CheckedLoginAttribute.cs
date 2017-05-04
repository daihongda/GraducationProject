using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GraduationProject.Common;
using DHD.ENTITY;
namespace GraduationProject.Filters
{
    public class CheckedLoginAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            int userId = GeneralCommon.getCurrentUserId();
            if (userId==0)
            {
                filterContext.Result = new RedirectResult("/Home/Index");
            }
        }
    }
}