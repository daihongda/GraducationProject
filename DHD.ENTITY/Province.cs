using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DHD.ENTITY
{
    public class Province
    {
         [DisplayName("省份id")]
        public int Id { get; set; }
         [DisplayName("省份名称 ")]
        public string Name { get; set; }
         public int IsDelete { get; set; }
    }
}
