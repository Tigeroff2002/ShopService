using Models;

namespace Logic.Abstractions;

public interface IOrderConfirmer
{
    public Task<bool> ConfirmAddressAsync(Order order, CancellationToken token);

    public Task<bool> ConfirmPayAsync(Order order, CancellationToken token);

    public Task<Order> ConfirmOrderAsync(Order order, CancellationToken token);
}
