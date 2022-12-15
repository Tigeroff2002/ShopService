using ShopService.Services;

namespace ShopService.Registrations;

public static class HostedService
{
    public static IServiceCollection AddHostedServices(this IServiceCollection services)
        => services
            .AddHostedService<OrderProcessorService>();
}
