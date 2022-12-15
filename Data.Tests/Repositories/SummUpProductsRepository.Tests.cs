using Xunit;

using FluentAssertions;

using Moq;

using Data.Repositories;
using Data.Contexts;
using Microsoft.Extensions.Logging;

namespace Data.Tests.Repositories;

public sealed class SummUpProductsRepositoryTests
{
    [Fact(DisplayName = $"{nameof(SummUpProductsRepository)} can be created")]
    [Trait("Category", "Unit")]
    public void CanBeCreated()
    {
        // Arrange
        var logger = Mock.Of<ILogger<SummUpProductsRepository>>();
        var context = Mock.Of<RetailStoreDataContext>();

        // Act
        var exception = Record.Exception(() => new SummUpProductsRepository(logger, context));

        // Assert
        exception.Should().BeNull();
    }


    [Fact(DisplayName = $"{nameof(SummUpProductsRepository)} can not be created because logger is null")]
    [Trait("Category", "Unit")]
    public void CanNotBeCreatedBecauseLoggerIsNull()
    {
        // Arrange
        var context = Mock.Of<RetailStoreDataContext>();

        // Act
        var exception = Record.Exception(() => new SummUpProductsRepository(null!, context));

        // Assert
        exception.Should().NotBeNull().And.BeOfType<ArgumentNullException>();
    }

    [Fact(DisplayName = $"{nameof(SummUpProductsRepository)} can not be created because context is null")]
    [Trait("Category", "Unit")]
    public void CanNotBeCreatedBecauseContextIsNull()
    {
        // Arrange
        var logger = Mock.Of<ILogger<SummUpProductsRepository>>();

        // Act
        var exception = Record.Exception(() => new SummUpProductsRepository(logger, null!));

        // Assert
        exception.Should().NotBeNull().And.BeOfType<ArgumentNullException>();
    }
}
