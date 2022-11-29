using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;

namespace AuthdService.Views.Account
{
    public class RegisterModel : PageModel
    {
        public RegisterModel() { }

        public RegisterModel(
            string email,
            string nickName,
            int roleType,
            string contactNumber,
            string password,
            string confirmPassword)
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

            Role = roleType;

            if (string.IsNullOrWhiteSpace(contactNumber))
                ContactNumber = "None";
            else
                ContactNumber = contactNumber;

            if (string.IsNullOrWhiteSpace(nickName))
                NickName = GetNameFromEmail();
            else
                NickName = nickName;

            if (!PasswordsEquals())
                throw new ArgumentException("Passwords doesnt equals!");
        }

        public string GetNameFromEmail()
        {
            return new MailAddress(Email!).User;
        }

        public bool PasswordsEquals()
        {
            return Password == ConfirmPassword;
        }
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            return Page();
        }

        [BindProperty]
        [Required]
        [Display(Name = "Адрес электронной почты")]
        public string? Email { get; private set; }

        [BindProperty]
        [Display(Name = "Имя пользователя (если хотите)")]
        public string? NickName { get; private set; }

        [BindProperty]
        public bool isRoleChoosed { get; private set; } = true;

        [BindProperty]
        [Display(Name = "Вариант роли пользователя")]
        public int Role { get; private set; }

        [BindProperty]
        [Required]
        [Display(Name = "Номер телефона")]
        public string? ContactNumber { get; private set; }

        [BindProperty]
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Введите пароль")]
        public string? Password { get; private set; }

        [BindProperty]
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Подтверждение пароля")]
        public string? ConfirmPassword { get; private set; }
    }
}
