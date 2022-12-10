using Logic.Abstractions;
using Models;

namespace Logic;

public sealed class OrderPay
    : IOrderPay
{
    public Task<Order> PayAsync(Order order, CancellationToken token)
    {
        throw new NotImplementedException();
    }
}
