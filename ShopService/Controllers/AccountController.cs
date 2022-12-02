using ShopService.Views.Account;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Models;
using Data.Contexts;
using System.Security.Claims;

namespace AuthdService.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AccountController : Controller
    {
        public AccountController(ILogger<AccountController> logger, RetailStoreDataContext context)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _context = context ?? throw new ArgumentNullException(nameof(context));

            _logger.LogInformation("Account Controller was started");

            //SeedSomeUserData();
        }

        [HttpPost("data/add")]
        public void SeedSomeUserData()
        {
            if (_context!.Clients.Count() < 4)
            {
                _context!.Clients.Add(new User(4, 1)
                {
                    Id = 4,
                    NickName = "TigeroffNew",
                    Password = "tigeroff2002",
                    ContactNumber = "+79042555027",
                    EmailAdress = "parahinkv2002@gmail.com",
                    TotalPurchase = 0f,
                    Discount = 0f,
                    Role = new Role(RoleType.AuthUser)
                });
            }

            _logger!.LogInformation("Fourth user was added to DB");
        }

        [HttpGet("login")]
        public async Task<ActionResult> Login()
        {
            var objLoginModel = new LoginModel();
            //return View("Login", objLoginModel);
            return View("Login", objLoginModel);
        }

        public async Task<ActionResult> LoginPost(
            string nickName,
            string password)
        {
            if (string.IsNullOrEmpty(nickName))
            {
                throw new ArgumentException(nameof(nickName));
            }

            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentException(nameof(password));
            }

            var objLoginModel = new LoginModel
            { 
                NickName = nickName, 
                Password = password 
            };

            if (ModelState.IsValid)
            {
                var user = _context!.Clients!
                    .Where(
                        x => x.NickName == objLoginModel.NickName &&
                        x.Password == objLoginModel.Password)
                    .FirstOrDefault();

                if (user == null)
                {
                    ViewBag.Message = "Such user was not registered in system yet";
                    return View("Register", new RegisterModel());
                }
                else
                {
                    var claims = new List<Claim>() {
                    new Claim(ClaimTypes.NameIdentifier, Convert.ToString(user.Id)),
                        new Claim(ClaimTypes.Name, user!.EmailAdress!),
                        new Claim(ClaimTypes.Role, user!.Role!.RoleCaption!)};

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var principal = new ClaimsPrincipal(identity);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties()
                    {
                        IsPersistent = objLoginModel.RememberLogin
                    });

                    _logger!.LogInformation("User was successfuly authorized!");

                    return RedirectToAction("Index", "Home", (new List<Product>(), user));
                }
            }

            return RedirectToAction("Index", "Home", (new List<Product>(), new User(1, 0)));
        }

        [HttpGet("logout")]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            _logger!.LogInformation("User was successfuly unauthorized!");

            return View("Login", new LoginModel());
        }

        [HttpGet("register")]
        public async Task<ActionResult> Register()
        {
            var objRegisterModel = new RegisterModel();
            return View("Register", objRegisterModel);
        }

        [HttpGet("postregister")]
        public async Task<IActionResult> RegisterPost(
            string email,
            string nickName,
            int roleType,
            string contactNumber,
            string password,
            string confirmPassword)
        {
            var objRegisterModel = new RegisterModel(
                email,
                nickName,
                roleType,
                contactNumber,
                password,
                confirmPassword);

            if (ModelState.IsValid)
            {
                User? user = new User(_context!.Clients.Count(), 1)
                {
                    Id = _context!.Clients.Count(),
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
                    if (CheckCurrentUserExistenseId(user) == -1)
                        _context!.Clients.Add(user);
                    else
                    {
                        ViewBag.Message = "Such user have already registered in system";
                        _logger!.LogInformation("User with these data was found in system!");
                        return View("Login", new LoginModel());
                    }

                    ArgumentNullException.ThrowIfNull(user!.EmailAdress);

                    ArgumentNullException.ThrowIfNull(user!.Role.RoleCaption);

                    var claims = new List<Claim>() {
                    new Claim(ClaimTypes.NameIdentifier, Convert.ToString(user!.Id)),
                        new Claim(ClaimTypes.Name, user!.EmailAdress!),
                        new Claim(ClaimTypes.Role, user!.Role!.RoleCaption!)};

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var principal = new ClaimsPrincipal(identity);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties()
                    {
                        IsPersistent = true
                    });

                    _logger!.LogInformation("User was successfuly registered in system!");

                    return RedirectToAction("Index", "Home", (new List<Product>(), user));
                }
                else
                {
                    ViewBag.Message = "Invalid Credential";
                    return View("Register", new RegisterModel());
                }
            }

            return RedirectToAction("Index", "Home", (new List<Product>(), new User(1, 0)));
        }

        private int CheckCurrentUserExistenseId(User? user)
        {
            if (_context!.Clients!.Any(x => x.Equals(user)))
                return _context!.Clients!.FirstOrDefault(x => x.Equals(user))!.Id;
            return -1;
        }

        private ILogger<AccountController>? _logger;
        private RetailStoreDataContext? _context;
    }
}