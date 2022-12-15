using Xunit;

using FluentAssertions;

using Moq;

using Logic;
using Microsoft.Extensions.Logging;
using Logic.Abstractions;
using Models;

namespace Logic.Tests;

public sealed class OrderConfirmerTests
{
    [Fact(DisplayName = $"{nameof(OrderConfirmer)} can be created")]
    [Trait("Category", "Unit")]
    public void CanBeCreated()
    {
        // Arrange
        var logger = Mock.Of<ILogger<OrderConfirmer>>();
        var payer = Mock.Of<IOrderPay>();

        // Act
        var exception = Record.Exception(() => new OrderConfirmer(logger, payer));

        // Assert
        exception.Should().BeNull();
    }

    [Fact(DisplayName = $"{nameof(OrderConfirmer)} can not be created because logger is null")]
    [Trait("Category", "Unit")]
    public void CanNotBeCreatedBecauseLoggerIsNull()
    {
        // Arrange
        var payer = Mock.Of<IOrderPay>();

        // Act
        var exception = Record.Exception(() => new OrderConfirmer(null!, payer));

        // Assert
        exception.Should().NotBeNull().And.BeOfType<ArgumentNullException>();
    }

    [Fact(DisplayName = $"{nameof(OrderConfirmer)} can not be created because payer is null")]
    [Trait("Category", "Unit")]
    public void CanNotBeCreatedBecausePayerIsNull()
    {
        // Arrange
        var logger = Mock.Of<ILogger<OrderConfirmer>>();

        // Act
        var exception = Record.Exception(() => new OrderConfirmer(logger, null!));

        // Assert
        exception.Should().NotBeNull().And.BeOfType<ArgumentNullException>();
    }

    [Fact(DisplayName = $"{nameof(OrderConfirmer)} can not confirm order when it is null")]
    [Trait("Category", "Unit")]
    public async Task CanNotConfirmWhenOrderIsNull()
    {
        // Arrange
        var logger = Mock.Of<ILogger<OrderConfirmer>>();
        var payer = new Mock<IOrderPay>(MockBehavior.Strict);

        var orderConfirmer = new OrderConfirmer(logger, payer.Object);
        var token = CancellationToken.None;

        // Act
        var exception = await Record.ExceptionAsync(
            async () => await orderConfirmer.ConfirmOrderAsync(null!, token));

        // Assert
        exception.Should().NotBeNull().And.BeOfType<ArgumentNullException>();
    }

    [Fact(DisplayName = $"{nameof(OrderConfirmer)} can not confirm order because operation is canceled")]
    [Trait("Category", "Unit")]
    public async Task CanNotConfirmBecauseOperationCancelled()
    {
        // Arrange
        var logger = Mock.Of<ILogger<OrderConfirmer>>();
        var payer = new Mock<IOrderPay>(MockBehavior.Strict);

        var orderConfirmer = new OrderConfirmer(logger, payer.Object);
        using var cts = new CancellationTokenSource();
        cts.Cancel();

        var order = new Mock<Order>();

        // Act
        var exception = await Record.ExceptionAsync(
            async() => await orderConfirmer.ConfirmOrderAsync(order.Object, cts.Token));

        // Assert
        exception.Should().NotBeNull().And.BeOfType<OperationCanceledException>();
    }

}
