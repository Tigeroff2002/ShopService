using Xunit;

using FluentAssertions;

using Moq;

using Models;

namespace Models.Tests;

public class OrderTests
{
    [Fact(DisplayName = $"{nameof(Order)} can be created")]
    [Trait("Category", "Unit")]
    public void CanBeCreated()
    {
        // Arrange
        var client = new Mock<User>(1, 1).Object;
        client.Basket = new Basket(new User(1, 1));

        // Act
        var exception = Record.Exception(() => new Order(client));

        // Assert
        exception.Should().BeNull();
    }


    [Fact(DisplayName = $"{nameof(Order)} can not be created cause client is null")]
    [Trait("Category", "Unit")]
    public void CanNotBeCreatedBecauseClientNull()
    {
        // Act
        var exception = Record.Exception(() => new Order(null!));

        // Assert
        exception.Should().NotBeNull().And.BeOfType<ArgumentNullException>(nameof(User));
    }

    [Fact(DisplayName = $"{nameof(Order)} can not be created cause client`s basket is null")]
    [Trait("Category", "Unit")]
    public void CanNotBeCreatedBecauseClientsBasketNull()
    {
        // Arrange
        var client = new Mock<User>(1, 1).Object;
        client.Basket = null!;

        // Act
        var exception = Record.Exception(() => new Order(client));

        // Assert
        exception.Should().NotBeNull().And.BeOfType<ArgumentNullException>(nameof(Basket));
    }
}
