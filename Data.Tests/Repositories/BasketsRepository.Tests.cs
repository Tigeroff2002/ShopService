using Xunit;

using FluentAssertions;

using Moq;

using Data.Repositories;
using Data.Contexts;
using Microsoft.Extensions.Logging;
using Data.Repositories.Abstractions;
using Data.Contexts.Abstractions;

namespace Data.Tests.Repositories;

public sealed class BasketsRepositoryTests
{
    [Fact(DisplayName = $"{nameof(BasketsRepository)} can be created")]
    [Trait("Category", "Unit")]
    public void CanBeCreated()
    {
        // Arrange
        var logger = Mock.Of<ILogger<BasketsRepository>>();
        var context = Mock.Of<IRepositoryContext>(MockBehavior.Strict);

        // Act
        var exception = Record.Exception(() => new BasketsRepository(logger, context));

        // Assert
        exception.Should().BeNull();
    }


    [Fact(DisplayName = $"{nameof(BasketsRepository)} can not be created because logger is null")]
    [Trait("Category", "Unit")]
    public void CanNotBeCreatedBecauseLoggerIsNull()
    {
        // Arrange
        var context = Mock.Of<IRepositoryContext>(MockBehavior.Strict);

        // Act
        var exception = Record.Exception(() => new BasketsRepository(null!, context));

        // Assert
        exception.Should().NotBeNull().And.BeOfType<ArgumentNullException>();
    }

    [Fact(DisplayName = $"{nameof(BasketsRepository)} can not be created because context is null")]
    [Trait("Category", "Unit")]
    public void CanNotBeCreatedBecauseContextIsNull()
{
        // Arrange
        var logger = Mock.Of<ILogger<BasketsRepository>>();

        // Act
        var exception = Record.Exception(() => new BasketsRepository(logger, null!));

        // Assert
        exception.Should().NotBeNull().And.BeOfType<ArgumentNullException>();
    }
}
