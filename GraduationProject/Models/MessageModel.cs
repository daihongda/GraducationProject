using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GraduationProject.Models
{
    public class MessageModel
    {
        [Required]
        public string Title { get; set; }
        [Required]
        [MinLength(10)]
        [MaxLength(200)]
        public string Content { get; set; }
        public int From { get; set; }
        public string FromName { get; set; }
        public string CreatedTime { get; set; }
        public int Id { get; set; }
    }
}