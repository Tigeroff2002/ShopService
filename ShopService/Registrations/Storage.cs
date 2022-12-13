using Data.Contexts;
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
            .AddSingleton<IBasketsRepository, BasketsRepository>()
            .AddSingleton<IClientsRepository, ClientsRepository>()
            .AddSingleton<IProductsRepository, ProductsRepository>()
            .AddSingleton<IShopsRepository, ShopsRepository>()
            .AddSingleton<ISummUpProductsRepository, SummUpProductsRepository>()
            .AddSingleton<IOrdersRepository, OrdersRepository>();
}
