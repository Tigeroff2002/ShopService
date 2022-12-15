using Xunit;

using FluentAssertions;

using Moq;

using Data.Repositories;
using Data.Contexts;
using Microsoft.Extensions.Logging;
using Data.Repositories.Abstractions;
using Data.Contexts.Abstractions;

namespace Data.Tests.Repositories;

public sealed class WarehousesRepositoryTests
{
    [Fact(DisplayName = $"{nameof(WarehousesRepository)} can be created")]
    [Trait("Category", "Unit")]
    public void CanBeCreated()
    {
        // Arrange
        var logger = Mock.Of<ILogger<WarehousesRepository>>();
        var context = Mock.Of<IRepositoryContext>();

        // Act
        var exception = Record.Exception(() => new WarehousesRepository(logger, context));

        // Assert
        exception.Should().BeNull();
    }


    [Fact(DisplayName = $"{nameof(WarehousesRepository)} can not be created because logger is null")]
    [Trait("Category", "Unit")]
    public void CanNotBeCreatedBecauseLoggerIsNull()
    {
        // Arrange
        var context = Mock.Of<IRepositoryContext>();

        // Act
        var exception = Record.Exception(() => new WarehousesRepository(null!, context));

        // Assert
        exception.Should().NotBeNull().And.BeOfType<ArgumentNullException>();
    }

    [Fact(DisplayName = $"{nameof(WarehousesRepository)} can not be created because context is null")]
    [Trait("Category", "Unit")]
    public void CanNotBeCreatedBecauseContextIsNull()
    {
        // Arrange
        var logger = Mock.Of<ILogger<WarehousesRepository>>();

        // Act
        var exception = Record.Exception(() => new WarehousesRepository(logger, null!));

        // Assert
        exception.Should().NotBeNull().And.BeOfType<ArgumentNullException>();
    }
}
