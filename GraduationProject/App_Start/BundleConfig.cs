using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace GraduationProject.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/Jq_Bootstrap").Include(
                        "~/Scripts/jquery-1.10.2.min.js",
                        "~/Scripts/bootstrap.min.js",
                        "~/Scripts/jquery.validate*",
                        "~/Scripts/jquery.validate.unobtrusive.min.js"));
            bundles.Add(new StyleBundle("~/Content/CommonCss").Include(
                      "~/Content/Css/bootstrap.css",
                      "~/Content/Js/dl-menu/component.css",
                      "~/Content/Css/slick.css",
                      "/Content/Css/slick-theme.css",
                      "~/Content/Assests/font-awesome/css/font-awesome.min.css",
                      "~/Content/Css/prettyPhoto.css",
                      "~/Content/Css/widget.css",
                      "~/Content/Css/shortcodes.css",
                      "~/Content/Css/style.css",
                      "~/Content/Css/color.css",
                      "~/Content/Css/responsive.css",
                      "~/Content/Css/bootstrap-multiselect.css"));
            //BundleTable.EnableOptimizations = true;
        }
    }
}