using Models;

namespace Logic.Abstractions;

public interface IOrderManager
{
    public Task<Order> ProcessAsync(Order order, CancellationToken cancellationToken);

    public Task ProcessOrdersAsync(CancellationToken cancellationToken);

    public Task<Order> CreateAsync(Order order, CancellationToken cancellationToken);

    public Task<Order> GiveOrderAsync(Order order, CancellationToken cancellationToken);

    public Task<Order> ConfirmOrderAsync(Order order, CancellationToken cancellationToken);

    public void CancelOrder(Order order, CancellationToken cancellationToken);
}
