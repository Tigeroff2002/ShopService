using Xunit;

using FluentAssertions;

using Moq;

using Models;

namespace Models.Tests
{
    public class OrderTests
    {
        [Fact(DisplayName = $"{nameof(Order)} can be created")]
        [Trait("Category", "Unit")]
        public void CanBeCreated()
        {
            // Arrange
            var shippingType = 1;
            var client = new Mock<User>(1, 1).Object;
            client.Basket = new Basket(1, new User(1, 1));

            // Act
            var exception = Record.Exception(() => new Order(shippingType, client));

            // Assert
            exception.Should().BeNull();
        }

        [Theory(DisplayName = $"{nameof(Order)} can not be created cause shippingType uncorrect")]
        [Trait("Category", "Unit")]
        [InlineData(0)]
        [InlineData(4)]
        public void CanNotBeCreatedBecauseShppingTypeUncorrect(int shippingType)
        {
            // Arrange
            var client = new Mock<User>(1, 1).Object;

            // Act
            var exception = Record.Exception(() => new Order(shippingType, client));

            // Assert
            exception.Should().NotBeNull().And.BeOfType<ArgumentException>();
        }

        [Fact(DisplayName = $"{nameof(Order)} can not be created cause client is null")]
        [Trait("Category", "Unit")]
        public void CanNotBeCreatedBecauseClientNull()
        {
            // Arrange
            var shippingType = 1;

            // Act
            var exception = Record.Exception(() => new Order(shippingType, null!));

            // Assert
            exception.Should().NotBeNull().And.BeOfType<ArgumentNullException>(nameof(User));
        }

        [Fact(DisplayName = $"{nameof(Order)} can not be created cause client`s basket is null")]
        [Trait("Category", "Unit")]
        public void CanNotBeCreatedBecauseClientsBasketNull()
        {
            // Arrange
            var shippingType = 1;
            var client = new Mock<User>(1, 1).Object;
            client.Basket = null!;

            // Act
            var exception = Record.Exception(() => new Order(shippingType, client));

            // Assert
            exception.Should().NotBeNull().And.BeOfType<ArgumentNullException>(nameof(Basket));
        }
    }
}
