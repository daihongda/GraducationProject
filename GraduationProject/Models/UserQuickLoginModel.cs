using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace GraduationProject.Models
{
    public class UserQuickLoginModel
    {
        [Required(ErrorMessage="手机验证码不可以为空！")]
        public string IdentifyingCode { get; set; }
        [Required(ErrorMessage="手机号码不可以为空")]
        public string PhoneNumber { get; set; }
    }
}