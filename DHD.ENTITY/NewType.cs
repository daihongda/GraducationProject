using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
namespace DHD.ENTITY
{
    public class NewType
    {
        [DisplayName("新闻类型ID")]
        public int Id { get; set; }
        [DisplayName("新闻类型名称")]
        public string Name { get; set; }
        public int IsDelete { get; set; }
    }
}
