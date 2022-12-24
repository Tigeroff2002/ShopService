using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

using Data.Contexts;

using Logic.Abstractions;
using Logic;

using Microsoft.AspNetCore.Authentication.Cookies;
using Serilog;

using Data.Repositories.Abstractions;
using Data.Repositories;
using Data.Contexts.Abstractions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);

builder.Services.AddRazorPages();

builder.Services.AddDbContext<RetailStoreDataContext>(opt =>
    opt.UseSqlServer($"Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=RetailStore;Integrated Security=True"))
    .AddScoped<IRepositoryContext, RepositoryContext>()
    .AddScoped<IBasketsRepository, BasketsRepository>()
    .AddScoped<IClientsRepository, ClientsRepository>()
    .AddScoped<IProductsRepository, ProductsRepository>()
    .AddScoped<IShopsRepository, ShopsRepository>()
    .AddScoped<ISummUpProductsRepository, SummUpProductsRepository>()
    .AddScoped<IWarehousesRepository, WarehousesRepository>()
    .AddScoped<IOrdersRepository, OrdersRepository>()
    .AddScoped<IOrderManager, OrderManager>()
    .AddScoped<IOrderConfirmer, OrderConfirmer>()
    .AddScoped<IOrderPay, OrderPay>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(x => x.LoginPath = "/login");

// Configuring Serilog

var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

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

app.MapRazorPages();

app.Run();