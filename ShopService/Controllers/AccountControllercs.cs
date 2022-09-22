using Microsoft.AspNetCore.Authentication;
using Auth0.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
namespace ShopService.Controllers
{
    public class AccountControllercs : Controller
    {
        public async Task Login(string returnUrl = "/")
        {
            var authentificationProperties = new LoginAuthenticationPropertiesBuilder()
                .WithRedirectUri(returnUrl)
                .Build();
            await HttpContext.ChallengeAsync(Auth0Constants.AuthenticationScheme, authentificationProperties)
        }
    }
}
