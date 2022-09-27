using Microsoft.AspNetCore.Authentication;
using Auth0.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization; 
using Microsoft.AspNetCore.Authentication.Cookies;


namespace ShopService.Controllers
{
    public class AccountControllercs : Controller
    {
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
