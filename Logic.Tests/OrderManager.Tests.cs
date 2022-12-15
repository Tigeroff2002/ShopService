using Microsoft.Extensions.Logging;

using Xunit;

using FluentAssertions;

using Moq;

using Logic;
using Logic.Abstractions;
using Data.Repositories.Abstractions;
using Models;

namespace Logic.Tests;

public sealed class OrderManagerTests
{
    [Fact(DisplayName = $"{nameof(OrderManager)} can be created")]
    [Trait("Category", "Unit")]
    public void CanBeCreated()
    {
        // Arrange
        var logger = Mock.Of<ILogger<OrderManager>>();
        var confirmer = Mock.Of<IOrderConfirmer>();
        var repository = Mock.Of<IOrdersRepository>();

        // Act
        var exception = Record.Exception(() => new OrderManager(logger, confirmer, repository));

        // Assert
        exception.Should().BeNull();
    }

    [Fact(DisplayName = $"{nameof(OrderManager)} can not be created because logger is null")]
    [Trait("Category", "Unit")]
    public void CanNotBeCreatedBecauseLoggerIsNull()
    {
        // Arrange
        var confirmer = Mock.Of<IOrderConfirmer>();
        var repository = Mock.Of<IOrdersRepository>();

        // Act
        var exception = Record.Exception(() => new OrderManager(null!, confirmer, repository));

        // Assert
        exception.Should().NotBeNull().And.BeOfType<ArgumentNullException>();
    }

    [Fact(DisplayName = $"{nameof(OrderManager)} can not be created because confirmer is null")]
    [Trait("Category", "Unit")]
    public void CanNotBeCreatedBecauseConfirmerIsNull()
    {
        // Arrange
        var logger = Mock.Of<ILogger<OrderManager>>();
        var repository = Mock.Of<IOrdersRepository>();

        // Act
        var exception = Record.Exception(() => new OrderManager(logger, null!, repository));

        // Assert
        exception.Should().NotBeNull().And.BeOfType<ArgumentNullException>();
    }

    [Fact(DisplayName = $"{nameof(OrderManager)} can not be created because repository is null")]
    [Trait("Category", "Unit")]
    public void CanNotBeCreatedBecauseRepositoryIsNull()
    {
        // Arrange
        var logger = Mock.Of<ILogger<OrderManager>>();
        var confirmer = Mock.Of<IOrderConfirmer>();

        // Act
        var exception = Record.Exception(() => new OrderManager(logger, confirmer, null!));

        // Assert
        exception.Should().NotBeNull().And.BeOfType<ArgumentNullException>();
    }


    [Fact(DisplayName = $"{nameof(OrderConfirmer)} can not confirm order when it is null")]
    [Trait("Category", "Unit")]
    public async Task CanNotConfirmWhenOrderIsNull()
    {
        // Arrange
        var logger = Mock.Of<ILogger<OrderManager>>();
        var confimer = new Mock<IOrderConfirmer>(MockBehavior.Strict);
        var repository = new Mock<IOrdersRepository>(MockBehavior.Strict);

        var orderManager= new OrderManager(logger, confimer.Object, repository.Object);
        var token = CancellationToken.None;

        // Act
        var exception = await Record.ExceptionAsync(
            async () => await orderManager.ProcessAsync(null!, token));

        // Assert
        exception.Should().NotBeNull().And.BeOfType<ArgumentNullException>();
    }

    [Fact(DisplayName = $"{nameof(OrderConfirmer)} can not confirm order because operation is canceled")]
    [Trait("Category", "Unit")]
    public async Task CanNotConfirmBecauseOperationCancelled()
    {
        // Arrange
        var logger = Mock.Of<ILogger<OrderManager>>();
        var confimer = new Mock<IOrderConfirmer>(MockBehavior.Strict);
        var repository = new Mock<IOrdersRepository>(MockBehavior.Strict);

        var orderManager = new OrderManager(logger, confimer.Object, repository.Object);
        using var cts = new CancellationTokenSource();
        cts.Cancel();

        var order = new Mock<Order>();

        // Act
        var exception = await Record.ExceptionAsync(
            async () => await orderManager.ProcessAsync(order.Object, cts.Token));

        // Assert
        exception.Should().NotBeNull().And.BeOfType<OperationCanceledException>();
    }
}
