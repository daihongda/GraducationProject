using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace GraduationProject.Models
{
    public class BackRegisterModel
    {
        [Display(Name="账号:")]
        [Required(ErrorMessage="用户名不可以为空！")]
        public string UserName { get; set; }

        [Display(Name = "昵称:")]
        [Required(ErrorMessage = "用户名不可以为空！")]
        public string NickName { get; set; }
         [Display(Name = "密码:")]
        [Required(ErrorMessage="密码不可以为空！")]

        public string Password { get; set; }
         [Display(Name = "确认:")]
        [Required(ErrorMessage="确认密码不可以为空")]
        [Compare("Password",ErrorMessage="两次密码输入不一致")]
        public string Password2 { get; set; }
    }
}