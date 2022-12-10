using Data.Contexts;
using Data.Repositories.Abstractions;
using Data.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

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
        _ = services.AddControllersWithViews();

        _ = services.AddRazorPages();

        _ = services.AddControllersWithViews().AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

        _ = services.AddDbContext<RetailStoreDataContext>(opt =>
                opt.UseSqlServer($"Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=RetailStore;Integrated Security=True"))
            .AddSingleton<IBasketsRepository, BasketsRepository>()
            .AddSingleton<IClientsRepository, ClientsRepository>()
            .AddSingleton<IOrdersRepository, OrdersRepository>()
            .AddSingleton<IProductsRepository, ProductsRepository>()
            .AddSingleton<IShopsRepository, ShopsRepository>()
            .AddSingleton<ISummUpProductsRepository, SummUpProductsRepository>();

        _ = services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(x => x.LoginPath = "/login");

        _ = services
            .AddLogging();
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

