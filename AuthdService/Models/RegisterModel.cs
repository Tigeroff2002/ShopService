﻿using System.ComponentModel.DataAnnotations;
using System.Net.Mail;

namespace AuthdService.Models
{
    public class RegisterModel
    {
        [Required]
        [Display(Name = "Email-adress")]
        public string? Email { get; set; }
        public string? NickName { get; set; }
        [Required]
        public string? ContactNumber { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string? ConfirmPassword { get; set; }

        public string? ReturnUrl { get; set; }

        public void CheckName()
        {
            if (string.IsNullOrEmpty(NickName))
            {
                MailAddress email = new MailAddress(Email!);
                NickName = email.User;
            }
        }
    }
}