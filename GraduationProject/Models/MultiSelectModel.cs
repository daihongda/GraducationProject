using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DHD.ENTITY;

namespace GraduationProject.Models
{
    public class MultiSelectModel
    {
        public List<Province> Provinces { get; set; }
        public int ProvinceId { get; set; }
        public List<Area> Areas { get; set; }
        public int AreaId { get; set; }
        public List<School> Schools { get; set; }
        public int PageCount { get; set; }
        public int CurrentPage { get; set; }
    }
}