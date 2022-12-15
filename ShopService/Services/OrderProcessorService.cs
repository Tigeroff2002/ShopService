using Logic.Abstractions;

namespace ShopService.Services;

public sealed class OrderProcessorService : BackgroundService
{
    public OrderProcessorService(
        ILogger<OrderProcessorService> logger, 
        IOrderManager manager)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger)); 
        _manager = manager ?? throw new ArgumentNullException(nameof(manager));

        _logger.LogInformation("OrderProcessorService was created just now");
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    => Task.Run(
        async () => await _manager.ProcessOrdersAsync(stoppingToken).ConfigureAwait(false),
            stoppingToken);

    private readonly ILogger<OrderProcessorService> _logger;
    private readonly IOrderManager _manager;
}
