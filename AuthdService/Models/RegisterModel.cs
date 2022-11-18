using ShopService.Models;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;

namespace AuthdService.Models
{
    public class RegisterModel
    {
        [Required]
        [Display(Name = "Email-adress")]
        public string? Email { get; set; }
        public string? NickName { get; set; }
        public bool isRoleChoosed { get; set; }
        public RoleType Role { get; set; }
        [Required]
        public string? ContactNumber { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string? ConfirmPassword { get; set; }

        public string? ReturnUrl { get; set; }

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
    }
}
