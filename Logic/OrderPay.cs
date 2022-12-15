using Microsoft.Extensions.Logging;

using Logic.Abstractions;
using Models;
using System.Security.Cryptography;

namespace Logic;

public sealed class OrderPay
    : IOrderPay
{
    public OrderPay(ILogger<OrderPay> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        _logger.LogInformation("OrderPay was created just now");
    }

    public async Task<bool> PayAsync(Order order, CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(order);

        token.ThrowIfCancellationRequested();

        if (order.isReadyForPayment)
        {
            await Task.Delay(5_000).ConfigureAwait(false);

            return RandomNumberGenerator.GetInt32(5) == 0 ? false : true;

        }

        return false;
    }

    private readonly ILogger<OrderPay> _logger;
}
