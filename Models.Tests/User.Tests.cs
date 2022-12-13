using Xunit;

using FluentAssertions;

using Moq;

using Models;

namespace Models.Tests;

public class UserTests
{
    [Fact(DisplayName = $"{nameof(User)} can be created")]
    [Trait("Category", "Unit")]
    public void CanBeCreated()
    {
        // Arrange
        var id = 1;
        var roleType = 1;

        // Act
        var exception = Record.Exception(() => new User(id, roleType));

        // Assert
        exception.Should().BeNull();
    }

    [Theory(DisplayName = $"{nameof(User)} can not be created cause id is uncorrect")]
    [Trait("Category", "Unit")]
    [InlineData(0)]
    [InlineData(-1)]
    public void CanNotBeCreatedBecauseIdUncorrect(int id)
    {
        // Arrange
        var roleType = 1;

        // Act
        var exception = Record.Exception(() => new User(id, roleType));

        // Assert
        exception.Should().NotBeNull().And.BeOfType<ArgumentException>();
    }

    [Theory(DisplayName = $"{nameof(User)} can not be created cause roleType is uncorrect")]
    [Trait("Category", "Unit")]
    [InlineData(0)]
    [InlineData(4)]
    public void CanNotBeCreatedBecauseRoletypeUncorrect(int roleType)
    {
        // Arrange
        var id = 1;

        // Act
        var exception = Record.Exception(() => new User(id, roleType));

        // Assert
        exception.Should().NotBeNull().And.BeOfType<ArgumentException>();
    }
}
