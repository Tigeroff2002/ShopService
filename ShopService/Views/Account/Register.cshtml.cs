using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;

namespace ShopService.Views.Account
{
    public class RegisterModel : PageModel
    {
        [Required]
        [Display(Name = "Адрес электронной почты")]
        public string? Email { get; private set; }
        [Display(Name = "Имя пользователя (если хотите)")]
        public string? NickName { get; private set; }
        public bool isRoleChoosed { get; private set; }
        public int roleType { get; private set; }
        [Required]
        [Display(Name = "Номер телефона")]
        public string? ContactNumber { get; private set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Введите пароль")]
        public string? Password { get; private set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Подтверждение пароля")]
        public string? ConfirmPassword { get; private set; }

        public string? ReturnUrl { get; } = "/Home/Index";

        public RegisterModel()
        {
            if (!isRoleChoosed)
                roleType = 1;
        }

        public RegisterModel(
            string email,
            string nickName,
            int roleType,
            string password,
            string confirmPassword,
            string contactNumber)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException(nameof(email));
            Email = email;

            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException(nameof(password));
            Password = password;

            if (string.IsNullOrWhiteSpace(confirmPassword))
                throw new ArgumentException(nameof(confirmPassword));
            ConfirmPassword = confirmPassword;

            if (!isRoleChoosed)
            {
                roleType = 1;
            }

            isRoleChoosed = true;

            if (string.IsNullOrWhiteSpace(contactNumber))
                ContactNumber = "None";
            else
                ContactNumber = contactNumber;

            if (string.IsNullOrWhiteSpace(nickName))
                NickName = GetNameFromEmail();
            else
                NickName = nickName;
            
            if (!ConfirmPassword.Equals(Password))
            {
                ConfirmPassword = Password;
            }
        }

        public string GetNameFromEmail()
        {
            return new MailAddress(Email!).User;
        }

        public void OnGet()
        {
        }
    }
}
