using Data.Repositories.Abstractions;
using Logic.Abstractions;
using Microsoft.AspNetCore.DataProtection.KeyManagement.Internal;
using Microsoft.Extensions.Logging;
using Models;
using System.Security.Cryptography;

namespace Logic;

public sealed class OrderManager
    : IOrderManager
{
    public OrderManager(
        ILogger<OrderManager> logger,
        IOrderConfirmer confirmer,
        IOrdersRepository repository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _confirmer = confirmer ?? throw new ArgumentNullException(nameof(confirmer));
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));

        _logger.LogInformation("OrderManager is created");
    }


    public async Task ProcessOrdersAsync(CancellationToken cancellationToken)
    {
        await Task.Delay(3_000, cancellationToken)
            .ConfigureAwait(false);

        _logger.LogInformation("Processor is starting now after 1s delay");
    }

    public async Task<Order> ProcessAsync(Order order, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(order);

        cancellationToken.ThrowIfCancellationRequested();

        _logger.LogInformation("OrderManager is begginning to process order...");

        var createdOrder = await CreateAsync(order, cancellationToken)
            .ConfigureAwait(false);

        _logger.LogInformation("OrderManager is pushing order to context within repository...");

        await _repository.AddOrderAsync(createdOrder, cancellationToken)
            .ConfigureAwait(false);

        _logger.LogInformation("OrderManager is confirming order...");

        var orderToConfirm = await _confirmer.ConfirmOrderAsync(order, cancellationToken)
            .ConfigureAwait(false);

        if (orderToConfirm.isPayd == true)
        {
            _logger.LogInformation("OrderConfirmer confirmed order succesfully");

            _logger.LogInformation("OrderManager is waiting when client will take his order...");

            var orderToGet = await GiveOrderAsync(orderToConfirm, cancellationToken)
                .ConfigureAwait(false);

            order.isGot = orderToGet.isGot;
        }

        if (orderToConfirm.isGot == false && orderToConfirm.isPayd == true)
        {
            _logger.LogInformation("OrderManager is automatically cancellating order...");

            CancelOrder(orderToConfirm, cancellationToken);

            orderToConfirm.isDeleted = true;
        }
        else if (orderToConfirm.isGot == true)
        {
            orderToConfirm.isDeleted = false;

            _logger.LogInformation("OrderManager is updating in context the order what was got by client...");

            _repository.UpdateOrder(orderToConfirm, cancellationToken);

            _repository.SaveChanges();
        }

        return orderToConfirm;
    }

    public async Task<Order> CreateAsync(Order order, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(order);

        cancellationToken.ThrowIfCancellationRequested();

        var orderAlreadyExists = await _repository.FindOrderAsync(order, cancellationToken)
            .ConfigureAwait(false) != null;

        if (!orderAlreadyExists)
        {
            if (order.ResultCost != 0 && order.SummUpProducts != null)
            {
                if (order.SummUpProducts.Count > 0)
                {
                    order.isReadyForConfirmation = true;

                    order.isReadyForPayment = false;

                    order.isPayd = false;

                    order.isGot = false;
                }
            }
        }

        _logger.LogInformation("OrderManager created the order");

        return order;
    }

    public async Task<Order> GiveOrderAsync(Order order, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(order);

        cancellationToken.ThrowIfCancellationRequested();

        _logger.LogInformation("Order is ready to get! Waiting for client...");

        var i = 0;

        while (i < 7)
        {
            if (RandomNumberGenerator.GetInt32(3) == 0)
            {
                break;
            }

            await Task.Delay(1_000)
                .ConfigureAwait(false);

            _logger.LogInformation($"Days gone {++i}");
        }

        if (i < 7)
        {
            _logger.LogInformation("OrderManager gave the order to client");

            order.isGot = true;

            return order;
        }

        _logger.LogInformation("OrderManager didnt give the order to client");

        order.isGot = false;

        return order;
    }

    public async Task<Order> ConfirmOrderAsync(Order order, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(order);

        cancellationToken.ThrowIfCancellationRequested();

        var taskWaiting = Task.Run(async () => await Task.Delay(10_000).ConfigureAwait(false));

        taskWaiting.Wait();

        return await _confirmer.ConfirmOrderAsync(order, cancellationToken)
            .ConfigureAwait(false);
    }
    public void CancelOrder(Order order, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(order);

        cancellationToken.ThrowIfCancellationRequested();

        _repository.UpdateOrder(order, cancellationToken);

        _logger.LogInformation("OrderManager cancelled and deleted the old order from db");

        _repository.SaveChanges();
    }

    private readonly ILogger<OrderManager> _logger;
    private readonly IOrderConfirmer _confirmer;
    private readonly IOrdersRepository _repository;
}
