using AuthdService.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using ShopService.Models;
using System.Security.Claims;

namespace AuthdService.Controllers
{
    public class AccountController : Controller
    {
        public List<User> users = new List<User>();
        public AccountController()
        {
            users.Add(new User()
            {
                Id = 1,
                NickName = "Tigeroff",
                Password = "tigeroff2002",
                ContactNumber = "+79042555027",
                EmailAdress = "parahinkv@gmail.com",
                TotalPurchase = 0f,
                Discount = 0f,
                Role = new Role(RoleType.AuthUser)
            });
            users.Add(new User()
            {
                Id = 2,
                NickName = "MLG",
                Password = "tigeroff2002",
                ContactNumber = "+79046485574",
                EmailAdress = "parahinvs@gmail.com",
                TotalPurchase = 0f,
                Discount = 0f,
                Role = new Role(RoleType.Manager)
            });
            users.Add(new User()
            {
                Id = 3,
                NickName = "Tigeroff2002",
                Password = "tigeroff2002",
                ContactNumber = "+79046594765",
                EmailAdress = "parahinnv@gmail.com",
                TotalPurchase = 0f,
                Discount = 0f,
                Role = new Role(RoleType.Admin)
            });
        }

        public IActionResult Login(string ReturnUrl = "/")
        {
            LoginModel objLoginModel = new LoginModel();
            objLoginModel.ReturnUrl = ReturnUrl;
            return View(objLoginModel);
        }

        public IActionResult Register(string ReturnUrl = "/")
        {
            RegisterModel objRegisterModel = new RegisterModel();
            objRegisterModel.ReturnUrl = ReturnUrl;
            return View(objRegisterModel);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel objLoginModel)
        {
            if (ModelState.IsValid)
            {
                var user = users.Where(x => x.NickName == objLoginModel.NickName && x.Password == objLoginModel.Password).FirstOrDefault();
                if (user == null)
                {
                    ViewBag.Message = "Invalid Credential";
                    return View(objLoginModel);
                }
                else
                {
                    var claims = new List<Claim>() {
                    new Claim(ClaimTypes.NameIdentifier, Convert.ToString(user.Id)),
                        new Claim(ClaimTypes.Name, user.NickName!),
                        new Claim(ClaimTypes.Role, user!.Role!.RoleCaption!)};
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var principal = new ClaimsPrincipal(identity);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties()
                    {
                        IsPersistent = objLoginModel.RememberLogin
                    });
                    return LocalRedirect(objLoginModel.ReturnUrl!);
                }
            }
            //return View(objLoginModel);
            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return LocalRedirect("/");
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel objRegisterModel)
        {
            if (ModelState.IsValid)
            {
                objRegisterModel.CheckName();
                User? user = new User()
                {
                    Id = users.Count + 1,
                    NickName = objRegisterModel.NickName,
                    Password = objRegisterModel?.Password,
                    ContactNumber = objRegisterModel?.ContactNumber,
                    EmailAdress = objRegisterModel?.Email,
                    TotalPurchase = 0f,
                    Discount = 0f,
                    Role = new Role(RoleType.AuthUser)
                };
                if (user != null)
                {
                    users.Add(user);
                    var claims = new List<Claim>() {
                    new Claim(ClaimTypes.NameIdentifier, Convert.ToString(user.Id)),
                        new Claim(ClaimTypes.Name, user.NickName!),
                        new Claim(ClaimTypes.Role, user!.Role!.RoleCaption!)};

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var principal = new ClaimsPrincipal(identity);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties()
                    {
                        IsPersistent = true
                    });
                    return LocalRedirect(objRegisterModel!.ReturnUrl!);
                }
                else
                {
                    ViewBag.Message = "Invalid Credential";
                    return View(objRegisterModel);
                }
            }
            return View(objRegisterModel);
        }
    }
}
