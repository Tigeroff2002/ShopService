using System.ComponentModel.DataAnnotations;
using System.Net.Mail;

namespace AuthdService.Models
{
    public class LoginModel
    {
        [Required]
        [Display(Name = "EmailAdress")]
        public string? Email { get; set; }
        [Display(Name = "Имя пользователя")]
        public string? NickName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string? Password { get; set; }
        [Display(Name = "Остаться в системе")]
        public bool RememberLogin { get; set; }
        public string? ReturnUrl { get; set; } = "/Home/Index";

        public LoginModel()
        {
        }

        public LoginModel(string email, string password)
        {
            Email = email;
            Password = password;
            RememberLogin = true;
            NickName = this.NickName;
        }
    }

}
