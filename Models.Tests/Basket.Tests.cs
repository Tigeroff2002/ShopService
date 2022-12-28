using Xunit;

using FluentAssertions;

using Moq;

using Models;

namespace Models.Tests;

public class BasketTests
{
    [Fact(DisplayName =$"{nameof(Basket)} can be created")]
    [Trait("Category", "Unit")]
    public void CanBeCreated()
    {
        // Arrange
        var client = new Mock<User>(1, 1).Object;

        // Act
        var exception = Record.Exception(() => new Basket(client));

        // Assert
        exception.Should().BeNull();
    }

    [Fact(DisplayName = $"{nameof(Basket)} can not be created cause client is null")]
    [Trait("Category", "Unit")]
    public void CanNotBeCreatedBecauseClientNull()
    {
        // Act
        var exception = Record.Exception(() => new Basket(null!));

        // Assert
        exception.Should().NotBeNull().And.BeOfType<ArgumentNullException>();
    }
}
