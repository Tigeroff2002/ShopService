using Logic.Abstractions;
using Models;

namespace Logic;

public sealed class OrderManager
    : IOrderManager
{
    public Task<Order> CreateAsync(Order order, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Order> GiveOrder(Order order, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Order> ProcessAsync(Order order, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
