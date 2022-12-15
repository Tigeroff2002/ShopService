using Xunit;

using FluentAssertions;

using Moq;

using Data.Repositories;
using Data.Contexts;
using Microsoft.Extensions.Logging;
using Data.Contexts.Abstractions;

namespace Data.Tests.Repositories;

public sealed class ProductsRepositoryTests
{
    [Fact(DisplayName = $"{nameof(ProductsRepository)} can be created")]
    [Trait("Category", "Unit")]
    public void CanBeCreated()
    {
        // Arrange
        var logger = Mock.Of<ILogger<ProductsRepository>>();
        var context = Mock.Of<IRepositoryContext>();

        // Act
        var exception = Record.Exception(() => new ProductsRepository(logger, context));

        // Assert
        exception.Should().BeNull();
    }


    [Fact(DisplayName = $"{nameof(ProductsRepository)} can not be created because logger is null")]
    [Trait("Category", "Unit")]
    public void CanNotBeCreatedBecauseLoggerIsNull()
    {
        // Arrange
        var context = Mock.Of<IRepositoryContext>();

        // Act
        var exception = Record.Exception(() => new ProductsRepository(null!, context));

        // Assert
        exception.Should().NotBeNull().And.BeOfType<ArgumentNullException>();
    }

    [Fact(DisplayName = $"{nameof(ProductsRepository)} can not be created because context is null")]
    [Trait("Category", "Unit")]
    public void CanNotBeCreatedBecauseContextIsNull()
    {
        // Arrange
        var logger = Mock.Of<ILogger<ProductsRepository>>();

        // Act
        var exception = Record.Exception(() => new ProductsRepository(logger, null!));

        // Assert
        exception.Should().NotBeNull().And.BeOfType<ArgumentNullException>();
    }
}
