using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DHD.ENTITY
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string NickName { get; set; }
        public string Password { get; set; }
        public int IsDelete { get; set; }
        public int SchoolId { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

    }
}
