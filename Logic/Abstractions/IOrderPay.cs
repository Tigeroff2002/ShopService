using Models;

namespace Logic.Abstractions;

public interface IOrderPay
{
    public Task<bool> PayAsync(Order order, CancellationToken token);
}
