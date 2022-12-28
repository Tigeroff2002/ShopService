using Logic.Abstractions;
using Microsoft.Extensions.Logging;
using Models;
using System.Security.Cryptography;

namespace Logic;

public sealed class OrderConfirmer
    : IOrderConfirmer
{
    public OrderConfirmer(
        ILogger<OrderConfirmer> logger,
        IOrderPay payer)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _payer = payer ?? throw new ArgumentNullException(nameof(payer));

        _logger.LogInformation("OrderConfimer has created");
    }

    public async Task<Order> ConfirmOrderAsync(Order order, CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(order);

        token.ThrowIfCancellationRequested();

        if (order.isReadyForConfirmation)
        {
            _logger.LogInformation("OrderConfirmer is beggining to confirm adress...");

            var shopAddressExistense = await ConfirmAddressAsync(order, token)
                       .ConfigureAwait(false);

            if (shopAddressExistense)
            {
                order.isReadyForPayment = true;

                _logger.LogInformation("OrderConfirmer confirmed adress");

                _logger.LogInformation("OrderConfirmer is beginning to confirm payment...");

                var payCompletion = await ConfirmPayAsync(order, token)
                    .ConfigureAwait(false);

                order.isPayd = payCompletion;

                _logger.LogInformation("OrderConfirmer checked the payment");
            }
            else
            {
                _logger.LogInformation("OrderConfirmer didnt confirm the adress");

                order.isReadyForPayment = false;

                order.isPayd = false;
            }
        }

        return order;
    }

    public async Task<bool> ConfirmAddressAsync(Order order, CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(order);

        token.ThrowIfCancellationRequested();

        await Task.Delay(5_000)
            .ConfigureAwait(false);

        return RandomNumberGenerator.GetInt32(5) == 0 ? false : true;
    }

    public async Task<bool> ConfirmPayAsync(Order order, CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(order);

        token.ThrowIfCancellationRequested();

        return await _payer.PayAsync(order, token).ConfigureAwait(false);
    }

    private readonly IOrderPay _payer;
    private readonly ILogger<OrderConfirmer> _logger;
}
