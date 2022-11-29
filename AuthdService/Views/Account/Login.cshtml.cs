using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace AuthdService.Views.Account
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        [Required]
        [Display(Name = "EmailAdress")]
        public string? Email { get; set; }

        [BindProperty]
        [Display(Name = "Имя пользователя")]
        public string? NickName { get; set; }

        [BindProperty]
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string? Password { get; set; }

        [BindProperty]
        [Display(Name = "Остаться в системе")]
        public bool RememberLogin { get; set; }

        public string? ReturnUrl { get; set; } = "/Home/Index";

        public LoginModel()
        {
        }
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            return Page();
        }
    }
}
