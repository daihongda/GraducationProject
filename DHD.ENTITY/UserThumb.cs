using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace DHD.ENTITY
{
    public class UserThumb
    {
         [Key, Column(Order = 1)]
        public int UserId { get; set; }
         [Key, Column(Order = 2)]
        public int SchoolNewId { get; set; }
        public DateTime OperateTime { get; set; }
        public int Type { get; set; }
    }
}
