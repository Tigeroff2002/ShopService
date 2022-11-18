using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AuthdService.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly ILogger<PrivacyModel> _logger;

        public RegisterModel(ILogger<PrivacyModel> logger)
        {
            _logger = logger;
        }
        public void OnGet()
        {
        }
    }
}
