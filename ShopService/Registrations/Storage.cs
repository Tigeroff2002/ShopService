using Data.Contexts;
using Data.Contexts.Abstractions;
using Data.Repositories;
using Data.Repositories.Abstractions;

using Microsoft.EntityFrameworkCore;

namespace ShopService.Registrations;

public static class Storage
{
    public static IServiceCollection AddStorage(this IServiceCollection services)
        => services
            .AddDbContext<RetailStoreDataContext>(opt =>
                opt.UseSqlServer($"Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=RetailStore;Integrated Security=True"))
            .AddScoped<IRepositoryContext, RepositoryContext>()
            .AddScoped<IBasketsRepository, BasketsRepository>()
            .AddScoped<IClientsRepository, ClientsRepository>()
            .AddScoped<IProductsRepository, ProductsRepository>()
            .AddScoped<IShopsRepository, ShopsRepository>()
            .AddScoped<ISummUpProductsRepository, SummUpProductsRepository>()
            .AddScoped<IOrdersRepository, OrdersRepository>();
}
