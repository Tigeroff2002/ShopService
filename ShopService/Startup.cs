using Data.Contexts;
using Data.Repositories.Abstractions;
using Data.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

using ShopService.Registrations;

namespace ShopService;

/// <summary>
/// Класс начальной конфигурации сервиса.
/// </summary>
public class Startup
{
    /// <summary>
    /// Создает новый экземпляр <see cref="Startup"/>.
    /// </summary>
    /// <param name="configuration">
    /// Конфигурация сервиса.
    /// </param>
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }

    /// <summary>
    /// Конфигурация.
    /// </summary>
    public IConfiguration Configuration { get; }

    /// <summary>
    /// Этот метод вызывается средой выполнения.
    /// </summary>
    public void ConfigureServices(IServiceCollection services)
    {
        _ = services
            .AddControllersWithViews()
            .AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

        _ = services
            .AddStorage()
            .AddOrderLogic();

        _ = services
            .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(x => x.LoginPath = "/login");

        _ = services
            .AddLogging()
            .AddRazorPages();
    }

    /// <summary>
    /// Этот метод вызывается средой выполнения.
    /// </summary>
    public void Configure(IApplicationBuilder app)
    {
        ArgumentNullException.ThrowIfNull(app);

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseDeveloperExceptionPage();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}"
            );
        });
    }
}

