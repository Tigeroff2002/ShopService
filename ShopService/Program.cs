using Microsoft.EntityFrameworkCore;
using ShopService.Data;
using Auth0.AspNetCore.Authentication;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<RetailStoreDataContext>(opt =>
    opt.UseSqlServer($"Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog = RetailStore; Integrated Security = True"));
builder.Services.AddAuth0WebAppAuthentication(
    opt =>
    {
        opt.Domain = builder.Configuration["Auth0:Domain"];
        opt.ClientId = builder.Configuration["ClientId"];
    });
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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();