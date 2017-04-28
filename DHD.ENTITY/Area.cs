using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace DHD.ENTITY
{
    public class Area
    {
         [DisplayName("市区Id")]
        public int Id { get; set; }
        [DisplayName("市区名称")]
        public string Name { get; set; }
        [ForeignKey("Province")]
        [DisplayName("省份名称")]
        public int ProvinceId { get; set; }
        public Province Province { get; set; }
        public int IsDelete { get; set; }
    }
}
