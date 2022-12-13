using Logic.Abstractions;
using Logic;

using Data.Repositories.Abstractions;
using Data.Repositories;

namespace ShopService.Registrations;

public static class Logic
{
    public static IServiceCollection AddOrderLogic(this IServiceCollection services)
        => services
            .AddSingleton<IOrdersRepository, OrdersRepository>()
            .AddSingleton<IOrderManager, OrderManager>()
            .AddSingleton<IOrderConfirmer, OrderConfirmer>()
            .AddSingleton<IOrderPay, OrderPay>();
}
