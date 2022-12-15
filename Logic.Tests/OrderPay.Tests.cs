using Microsoft.Extensions.Logging;

using Xunit;

using FluentAssertions;

using Moq;

using Logic;
using Models;

namespace Logic.Tests;

public sealed class OrderPayTests
{
    [Fact(DisplayName = $"{nameof(OrderPay)} can be created")]
    [Trait("Category", "Unit")]
    public void CanBeCreated()
    {
        // Arrange
        var logger = Mock.Of<ILogger<OrderPay>>();

        // Act
        var exception = Record.Exception(() => new OrderPay(logger));

        // Assert
        exception.Should().BeNull();
    }

    [Fact(DisplayName = $"{nameof(OrderPay)} can not be created because logger is null")]
    [Trait("Category", "Unit")]
    public void CanNotBeCreatedBecauseLoggerIsNull()
    {
        // Act
        var exception = Record.Exception(() => new OrderPay(null!));

        // Assert
        exception.Should().NotBeNull().And.BeOfType<ArgumentNullException>();
    }

    [Fact(DisplayName = $"{nameof(OrderPay)} can not confirm order when it is null")]
    [Trait("Category", "Unit")]
    public async Task CanNotConfirmWhenOrderIsNull()
    {
        // Arrange
        var logger = Mock.Of<ILogger<OrderPay>>();

        var orderPayer = new OrderPay(logger);
        var token = CancellationToken.None;

        // Act
        var exception = await Record.ExceptionAsync(
            async() => await orderPayer.PayAsync(null!, token));

        // Assert
        exception.Should().NotBeNull().And.BeOfType<ArgumentNullException>();
    }

    [Fact(DisplayName = $"{nameof(OrderPay)} can not confirm order because operation is canceled")]
    [Trait("Category", "Unit")]
    public async Task CanNotConfirmBecauseOperationCancelled()
    {
        // Arrange
        var logger = Mock.Of<ILogger<OrderPay>>();

        var orderPayer = new OrderPay(logger);
        using var cts = new CancellationTokenSource();
        cts.Cancel();

        var order = new Mock<Order>();

        // Act
        var exception = await Record.ExceptionAsync(
            async() => await orderPayer.PayAsync(order.Object, cts.Token));

        // Assert
        exception.Should().NotBeNull().And.BeOfType<OperationCanceledException>();
    }
}
