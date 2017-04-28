using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DHD.ENTITY
{
    public class Message
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int From { get; set; }
        public int To { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}
