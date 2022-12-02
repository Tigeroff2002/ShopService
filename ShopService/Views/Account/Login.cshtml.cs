using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ShopService.Views.Account
{
    public class LoginModel : PageModel
    {
        [Required]
        [Display(Name = "EmailAdress")]
        public string? Email { get; set; }
        [Display(Name = "��� ������������")]
        public string? NickName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "������")]
        public string? Password { get; set; }
        [Display(Name = "�������� � �������")]
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
        public void OnGet()
        {
        }
    }
}
