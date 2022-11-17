using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AuthdService.Pages.Shared
{
    public class PrivateDataModel : PageModel
    {
        private readonly ILogger<PrivacyModel> _logger;

        public PrivateDataModel(ILogger<PrivacyModel> logger)
        {
            _logger = logger;
        }
        public void OnGet()
        {
        }
    }
}
