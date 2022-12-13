using Xunit;

using FluentAssertions;

using Moq;

using Models;

namespace Models.Tests;

public class ShippingTests
{
    [Theory(DisplayName =$"{nameof(Shipping)} can be created")]
    [Trait("Category", "Unit")]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public void CanBeCreated(int shipType)
    {
        // Act
        var exception = Record.Exception(() => new Shipping(shipType));

        // Assert
        exception.Should().BeNull();
    }

    [Theory(DisplayName = $"{nameof(Shipping)} can not be created because shipType is uncorrect")]
    [Trait("Category", "Unit")]
    [InlineData(-1)]
    [InlineData(0)]
    [InlineData(4)]
    public void CanNotBeCreatedBecauseShiptypeUncorrect(int shipType)
    {
        // Act
        var exception = Record.Exception(() => new Shipping(shipType));

        // Assert
        exception.Should().NotBeNull().And.BeOfType<ArgumentException>(nameof(Shipping));
    }

    [Fact(DisplayName = $"{nameof(Shipping)} can correctly calculate price of shipping")]
    [Trait("Category", "Unit")]
    public void CanCalculateShippingCost()
    {
        // Arrange
        var shipType = 2;
        var cost = 1000;
        var shippingMock = new Mock<Shipping>(shipType);

        // Act
        var expectedResult = (int)shippingMock.Object.ShipType * cost;
        var result = shippingMock.Object.CalculateShippingCost();

        // Assert
        result.Should().Be(expectedResult);
    }
}
