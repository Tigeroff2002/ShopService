using Xunit;

using FluentAssertions;

using Moq;

using Data.Repositories;
using Data.Contexts;
using Microsoft.Extensions.Logging;

namespace Data.Tests.Repositories;

public sealed class ClientsRepositoryTests
{
    [Fact(DisplayName = $"{nameof(ClientsRepository)} can be created")]
    [Trait("Category", "Unit")]
    public void CanBeCreated()
    {
        // Arrange
        var logger = Mock.Of<ILogger<ClientsRepository>>();
        var context = Mock.Of<RetailStoreDataContext>();

        // Act
        var exception = Record.Exception(() => new ClientsRepository(logger, context));

        // Assert
        exception.Should().BeNull();
    }


    [Fact(DisplayName = $"{nameof(ClientsRepository)} can not be created because logger is null")]
    [Trait("Category", "Unit")]
    public void CanNotBeCreatedBecauseLoggerIsNull()
    {
        // Arrange
        var context = Mock.Of<RetailStoreDataContext>();

        // Act
        var exception = Record.Exception(() => new ClientsRepository(null!, context));

        // Assert
        exception.Should().NotBeNull().And.BeOfType<ArgumentNullException>();
    }

    [Fact(DisplayName = $"{nameof(ClientsRepository)} can not be created because context is null")]
    [Trait("Category", "Unit")]
    public void CanNotBeCreatedBecauseContextIsNull()
    {
        // Arrange
        var logger = Mock.Of<ILogger<ClientsRepository>>();

        // Act
        var exception = Record.Exception(() => new ClientsRepository(logger, null!));

        // Assert
        exception.Should().NotBeNull().And.BeOfType<ArgumentNullException>();
    }
}
