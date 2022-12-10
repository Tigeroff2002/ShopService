using Models;

namespace Logic.Abstractions;

public interface IOrderManager
{
    public Task<Order> ProcessAsync(Order order, CancellationToken cancellationToken);

    public Task<Order> CreateAsync(Order order, CancellationToken cancellationToken);

    public Task<Order> GiveOrder(Order order, CancellationToken cancellationToken); 
}
