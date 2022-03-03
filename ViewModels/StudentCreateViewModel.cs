﻿using DemoApplication.Models;
using System.ComponentModel.DataAnnotations;

namespace DemoApplication.ViewModels
{
    public class StudentCreateViewModel
    {
        public int Id { get; set; }

        [Display(Name = "姓名")]
        [Required(ErrorMessage = "input name"), MaxLength(50, ErrorMessage = "不能超过10")]
        public string Name { get; set; }

        [Display(Name = "班级")]
        [Required]
        public ClassNameEnum? ClassName { get; set; }

        [Display(Name = "邮箱")]
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$",
         ErrorMessage = "格式不正确")]
        public string Email { get; set; }

        [Display(Name="图片")]
        public IFormFile? Photo { get; set; }
    }
}
