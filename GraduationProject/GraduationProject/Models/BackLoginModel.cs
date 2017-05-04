using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace GraduationProject.Models
{
    public class BackLoginModel
    {
        [Display(Name="账号:")]
        [Required(ErrorMessage="用户名不可以为空！")]
        public string UserName { get; set; }
        [Display(Name = "密码:")]
        [Required(ErrorMessage = "密码不可以为空！")]
        public string Password { get; set; }
    }
}