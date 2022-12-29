using ShopService.Views.Account;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

using Models;

using Data.Repositories.Abstractions;

using System.Security.Claims;

namespace AuthdService.Controllers;

[ApiController]
[Route("api/account")]
public class AccountController : Controller
{
    public AccountController(
        ILogger<AccountController> logger,
        IClientsRepository repository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));

        _logger.LogInformation("Account Controller was started just now");
    }

    [HttpGet("login")]
    public IActionResult Login()
    {
        var objLoginModel = new LoginModel();

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
            var token = CancellationToken.None;

            var user = await _repository.FindNickNameAsync(objLoginModel.NickName, token)
                .ConfigureAwait(false);

            if (user == null)
            {
                ViewBag.Message = "Such user was not registered in system yet";
                return View("Register", new RegisterModel());
            }
            else
            {
                var claims = new List<Claim>() {
                new Claim(ClaimTypes.NameIdentifier, Convert.ToString(user.UserId)),
                    new Claim(ClaimTypes.Name, user!.EmailAdress!),
                    new Claim(ClaimTypes.Role, user!.Role!.RoleCaption!)};

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties()
                {
                    IsPersistent = objLoginModel.RememberLogin
                });

                _logger!.LogInformation("User was successfuly authorized!");

                return RedirectToAction("AuthIndex", "Home", new {id = user.UserId});
            }
        }

        return RedirectToAction("Index", "Home");
    }

    [HttpGet("logout")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        _logger!.LogInformation("User was successfuly unauthorized!");

        return RedirectToAction(
            "Index", 
            "Home",
            (new List<Product>(),
            new User
            {
                Id = -1,
                Role = new Role(RoleType.NonAuthUser)
            },
            new Product()));
    }

    [HttpGet("register")]
    public IActionResult Register()
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
            var user = new User()
            {
                UserId = _repository.UserCount + 1,
                NickName = objRegisterModel.NickName,
                Password = objRegisterModel?.Password,
                ContactNumber = objRegisterModel?.ContactNumber,
                EmailAdress = objRegisterModel?.Email,
                TotalPurchase = 0f,
                Discount = 0f,
                Role = new Role((RoleType) roleType)
            };

            if (user!.Role.RoleType != RoleType.AuthUser 
                && user!.Role.RoleType != RoleType.Manager 
                && user!.Role.RoleType != RoleType.Admin)
            {
                user.Role.RoleType = RoleType.AuthUser;
            }


            if (user != null)
            {
                var existense = await CheckCurrentUserExistense(user);

                if (existense)
                {
                    ViewBag.Message = "Such user have already registered in system";
                    _logger!.LogInformation("User with these data was found in system!");

                    return View("Login", new LoginModel());
                }

                ArgumentNullException.ThrowIfNull(user!.Role);

                if (string.IsNullOrWhiteSpace(user!.EmailAdress))
                {
                    throw new ArgumentException();
                }

                if (string.IsNullOrWhiteSpace(user!.NickName))
                {
                    throw new ArgumentException();
                }

                await _repository.AddAsync(user!, CancellationToken.None);

                _repository.SaveChanges();

                var claims = new List<Claim>() 
                {
                    new Claim(ClaimTypes.NameIdentifier, Convert.ToString(user!.UserId)),
                    new Claim(ClaimTypes.Name, user!.EmailAdress!),
                    new Claim(ClaimTypes.Role, user!.Role!.RoleCaption!
                )};

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme, 
                    principal, 
                    new AuthenticationProperties()
                    {
                        IsPersistent = true
                    });

                _logger!.LogInformation("User was successfuly registered in system!");

                return RedirectToAction("AuthIndex", "Home", new { id = user.UserId });
            }
            else
            {
                ViewBag.Message = "Invalid Credential";
                return View("Register", new RegisterModel());
            }
        }

        return RedirectToAction("Index", "Home", (new List<Product>(), new User(1, 0), new Product()));
    }

    [HttpPost("data/add")]
    private void SeedSomeUserData()
    {
        if (_repository.UserCount < 4)
        {
            _repository.AddAsync(new User(4, 1)
            {
                Id = 4,
                NickName = "TigeroffNew",
                Password = "tigeroff2002",
                ContactNumber = "+79042555027",
                EmailAdress = "parahinkv2002@gmail.com",
                TotalPurchase = 0f,
                Discount = 0f,
                Role = new Role(RoleType.AuthUser)
            }, CancellationToken.None);
        }

        _logger!.LogInformation("Fourth user was added to DB");
    }

    private async Task<bool> CheckCurrentUserExistense(User? user)
    {
        ArgumentNullException.ThrowIfNull(user);

        ArgumentNullException.ThrowIfNull(user.EmailAdress);

        return await _repository
            .FindEmailAsync(user.EmailAdress, CancellationToken.None) != null;
    }

    private readonly ILogger<AccountController> _logger;
    private readonly IClientsRepository _repository;
}