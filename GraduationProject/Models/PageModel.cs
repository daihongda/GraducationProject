using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GraduationProject.Models
{
    public class PageModel
    {
        public int PageCount { get; set; }
        public int CurrentPage { get; set; }
        public object Data { get; set; }
    }
}