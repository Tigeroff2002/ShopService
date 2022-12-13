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
        var statusId = 1;
        var client = new Mock<User>(1, 1).Object;

        // Act
        var exception = Record.Exception(() => new Basket(statusId, client));

        // Assert
        exception.Should().BeNull();
    }

    [Fact(DisplayName = $"{nameof(Basket)} can not be created cause client is null")]
    [Trait("Category", "Unit")]
    public void CanNotBeCreatedBecauseClientNull()
    {
        // Arrange
        var statusId = 1;

        // Act
        var exception = Record.Exception(() => new Basket(statusId, null!));

        // Assert
        exception.Should().NotBeNull().And.BeOfType<ArgumentNullException>();
    }

    [Theory(DisplayName = $"{nameof(Basket)} can not be created cause client is null")]
    [Trait("Category", "Unit")]
    [InlineData(-100)]
    [InlineData(-1)]
    [InlineData(0)]
    public void CanNotBeCreatedBecauseStatusUncorrect(int statusId)
    {
        // Arrange
        var client = new Mock<User>(1, 1).Object;

        // Act
        var exception = Record.Exception(() => new Basket(statusId, client));

        // Assert
        exception.Should().NotBeNull().And.BeOfType<ArgumentException>();
    }
}
