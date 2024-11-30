﻿using System.ComponentModel.DataAnnotations;

namespace MyWebsite.UI.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Tên người dùng không được để trống")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Email không đúng định dạng")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        [MinLength(6, ErrorMessage = "Mật khẩu phải có ít nhất 6 ký tự")]
        public string Password { get; set; }

        public string? ReturnUrl { get; set; }

        public bool RememberMe { get; set; }
    }
}
