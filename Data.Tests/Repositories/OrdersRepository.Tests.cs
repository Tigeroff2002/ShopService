using Xunit;

using FluentAssertions;

using Moq;

using Data.Repositories;
using Microsoft.Extensions.Logging;
using Data.Repositories.Abstractions;
using Data.Contexts;

namespace Data.Tests.Repositories;

public sealed class OrdersRepositoryTests
{
    [Fact(DisplayName = $"{nameof(OrdersRepository)} can be created")]
    [Trait("Category", "Unit")]
    public void CanBeCreated()
    {
        // Arrange
        var logger = Mock.Of<ILogger<OrdersRepository>>();
        var context = Mock.Of<RetailStoreDataContext>();

        // Act
        var exception = Record.Exception(() => new OrdersRepository(logger, context));

        // Assert
        exception.Should().BeNull();
    }


    [Fact(DisplayName = $"{nameof(OrdersRepository)} can not be created because logger is null")]
    [Trait("Category", "Unit")]
    public void CanNotBeCreatedBecauseLoggerIsNull()
    {
        // Arrange
        var context = Mock.Of<RetailStoreDataContext>();

        // Act
        var exception = Record.Exception(() => new OrdersRepository(null!, context));

        // Assert
        exception.Should().NotBeNull().And.BeOfType<ArgumentNullException>();
    }

    [Fact(DisplayName = $"{nameof(OrdersRepository)} can not be created because context is null")]
    [Trait("Category", "Unit")]
    public void CanNotBeCreatedBecauseContextIsNull()
    {
        // Arrange
        var logger = Mock.Of<ILogger<OrdersRepository>>();

        // Act
        var exception = Record.Exception(() => new OrdersRepository(logger, null!));

        // Assert
        exception.Should().NotBeNull().And.BeOfType<ArgumentNullException>();
    }
}
