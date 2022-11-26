using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using Models;

namespace AuthdService.Models
{
    public class RegisterModel
    {
        [Required]
        [Display(Name = "Адрес электронной почты")]
        public string? Email { get; set; }
        [Display(Name = "Имя пользователя (если хотите)")]
        public string? NickName { get; set; }
        public bool isRoleChoosed { get; set; }
        public RoleType Role { get; set; }
        [Required]
        [Display(Name = "Номер телефона")]
        public string? ContactNumber { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Введите пароль")]
        public string? Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Подтверждение пароля")]
        public string? ConfirmPassword { get; set; }

        public string? ReturnUrl { get; set; } = "/Home/Index";

        public RegisterModel()
        {
            if (!isRoleChoosed)
                Role = RoleType.AuthUser;
        }
        public void CheckName()
        {
            if (string.IsNullOrEmpty(NickName))
            {
                MailAddress email = new MailAddress(Email!);
                NickName = email.User;
            }
        }

        public bool PasswordsEquals()
        {
            return Password == ConfirmPassword;
        }
    }
}
