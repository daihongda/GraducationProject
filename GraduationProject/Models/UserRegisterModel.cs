using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GraduationProject.Models
{
    public class UserRegisterModel
    {
        [Required(ErrorMessage="用户名不可以为空!")]
        public string Username { get; set; }
        [Required(ErrorMessage="用户昵称不可为空!")]
        public string NickName { get; set; }
        [Required(ErrorMessage="电话号码不可以为空!")]
        [RegularExpression(@"^[1][358][0-9]{9}$",
                   ErrorMessage="电话号码格式不正确!")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage="密码不可以为空！")]
        public string Password { get; set; }
        [Compare("Password",ErrorMessage="两次密码输入不一致！")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage="邮箱不可以为空!")]
        [EmailAddress(ErrorMessage="邮箱格式不正确！")]
        public string Email { get; set; }
        [Required(ErrorMessage="请选择你的学校！")]
        public int SchoolId { get; set; }
    }
}