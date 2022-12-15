using Models;

namespace Logic.Abstractions;

public interface IOrderManager
{
    public Task<Order> ProcessAsync(Order order, CancellationToken cancellationToken);

    public Task ProcessOrdersAsync(CancellationToken cancellationToken);

    public Task<Order> CreateAsync(Order order, CancellationToken cancellationToken);

    public Task<bool> GiveOrder(Order order, CancellationToken cancellationToken);

    public Task CancelOrderAsync(Order order, CancellationToken cancellationToken);
}
