using Models;

namespace Logic.Abstractions;

public interface IOrderPay
{
    public Task<Order> PayAsync(Order order, CancellationToken token);
}
