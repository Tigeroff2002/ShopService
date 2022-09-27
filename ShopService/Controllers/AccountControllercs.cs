using Microsoft.AspNetCore.Authentication;
using Auth0.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization; 
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using ShopService.Models;


namespace ShopService.Controllers
{
    public class AccountControllercs : Controller
    {
        [Authorize]
        public IActionResult Profile()
        {
            return View(new ProfileModel()
            {
                Id = User.FindFirst(x => x.Type == ClaimTypes.Authentication)?.Value,
                Name = User.Identity!.Name,
                EmailAdress = User.FindFirst(x => x.Type == ClaimTypes.Email)?.Value,

            });
        }
        public async Task Login(string returnUrl = "/")
        {
            var authentificationProperties = new LoginAuthenticationPropertiesBuilder()
                .WithRedirectUri(returnUrl)
                .Build();
            await HttpContext.ChallengeAsync(Auth0Constants.AuthenticationScheme, authentificationProperties);
        }
        [Authorize]
        public async Task Logout()
        {
            var authentificationProperties = new LogoutAuthenticationPropertiesBuilder()
                .WithRedirectUri(Url.Action("Index", "Home"))
                .Build();
            await HttpContext.SignOutAsync(Auth0Constants.AuthenticationScheme, authentificationProperties);
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}
