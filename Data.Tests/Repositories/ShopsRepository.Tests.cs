using Xunit;

using FluentAssertions;

using Moq;

using Data.Repositories;
using Data.Contexts;
using Microsoft.Extensions.Logging;
using Data.Repositories.Abstractions;

namespace Data.Tests.Repositories;

public sealed class ShopsRepositoryTests
{
    [Fact(DisplayName = $"{nameof(ShopsRepository)} can be created")]
    [Trait("Category", "Unit")]
    public void CanBeCreated()
    {
        // Arrange
        var logger = Mock.Of<ILogger<ShopsRepository>>();
        var context = Mock.Of<RetailStoreDataContext>();

        // Act
        var exception = Record.Exception(() => new ShopsRepository(logger, context));

        // Assert
        exception.Should().BeNull();
    }


    [Fact(DisplayName = $"{nameof(ShopsRepository)} can not be created because logger is null")]
    [Trait("Category", "Unit")]
    public void CanNotBeCreatedBecauseLoggerIsNull()
    {
        // Arrange
        var context = Mock.Of<RetailStoreDataContext>();

        // Act
        var exception = Record.Exception(() => new ShopsRepository(null!, context));

        // Assert
        exception.Should().NotBeNull().And.BeOfType<ArgumentNullException>();
    }

    [Fact(DisplayName = $"{nameof(ShopsRepository)} can not be created because context is null")]
    [Trait("Category", "Unit")]
    public void CanNotBeCreatedBecauseContextIsNull()
    {
        // Arrange
        var logger = Mock.Of<ILogger<ShopsRepository>>();

// Act
        var exception = Record.Exception(() => new ShopsRepository(logger, null!));

        // Assert
        exception.Should().NotBeNull().And.BeOfType<ArgumentNullException>();
    }
}
