using Microsoft.AspNetCore.Authentication;
using Auth0.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization; 
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using ShopService.Models;
using ShopService.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace ShopService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfileController : ControllerBase
    {
        private readonly ILogger<ProfileController> _logger;    
        private readonly RetailStoreProfileContext? _context;

        private readonly string? _registrationServiceAdress = "https://localhost:1111/api/register";
        public ProfileController(ILogger<ProfileController> logger, RetailStoreProfileContext? context)
        {
            _context = context;
            _logger = logger;
        }

        private JsonSerializerOptions options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };

        /*
        [HttpGet]
        [Authorize]
        public IActionResult Profile()
        {
            return View(new User()
            {
                Id = Convert.ToInt32(User.FindFirst(x => x.Type == ClaimTypes.Authentication)?.Value),
                NickName = User.Identity!.Name,
                EmailAdress = User.FindFirst(x => x.Type == ClaimTypes.Email)?.Value
            });
        }
        */
        [HttpPost]
        public async Task Login(string returnUrl = "/")
        {
            var authentificationProperties = new LoginAuthenticationPropertiesBuilder()
                .WithRedirectUri(returnUrl)
                .Build();
            if (!User!.Identity!.IsAuthenticated)
                await HttpContext.ChallengeAsync(Auth0Constants.AuthenticationScheme, authentificationProperties);
        }
        [Authorize]
        public async Task Logout()
        {
            var authentificationProperties = new LogoutAuthenticationPropertiesBuilder()
                .WithRedirectUri(Url.Action("Index", "Home")!)
                .Build();
            await HttpContext.SignOutAsync(Auth0Constants.AuthenticationScheme, authentificationProperties);
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
        [HttpPost]
        public async Task<ActionResult<User>> GetRegistratedUser()
        {
            var clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert,
                chain, sslpolicyErrors) =>
                    { return true; };
            using (var client = new HttpClient(clientHandler))
            {
                HttpResponseMessage response = await client.GetAsync($"{_registrationServiceAdress}");
                if (response.IsSuccessStatusCode)
                {
                    return await JsonSerializer.DeserializeAsync<User>(
                        await response.Content.ReadAsStreamAsync());
                }
            }
            return null!;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context!.Users.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context!.Users.FindAsync(id);
            if (user == null)
                return NotFound();

            return user;
        }

        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User? user)
        {
            _context!.Users.Add(user!);
            await _context!.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user!.Id }, user);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            var user = await _context!.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<User>> RoleForUser(int id)
        {
            var user = await _context!.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context!.Users.Where(x => x.Id == id)
                           .ToList()
                           .Select(x => 
                            {
                                x.Role = new Role { RoleType = RoleType.AuthUser};
                                return x;
                            });
            await _context!.SaveChangesAsync();

            return NoContent();
        }
    }
}
