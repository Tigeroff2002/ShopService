using Xunit;

using FluentAssertions;

using Moq;

using Models;

namespace Models.Tests
{
    public class SummUpProductTests
    {
        [Fact(DisplayName = $"{nameof(SummUpProduct)} can be created")]
        [Trait("Category", "Unit")]
        public void CanBeCreated()
        {
            // Arrange
            var id = 10;
            var quantity = 5;
            var deviceType = new Mock<DeviceType>(1).Object;
            var producer = new Mock<Producer>().Object;

            var product = new Mock<Product>(1, deviceType, producer).Object;

            // Act
            var exception = Record.Exception(() => new SummUpProduct(id, product, quantity));

            // Assert
            exception.Should().BeNull();
        }

        [Fact(DisplayName = $"{nameof(SummUpProduct)} can not be created with null product")]
        [Trait("Category", "Unit")]
        public void CanNotBeCreatedBecauseProductNull()
        {
            // Arrange
            var id = 10;
            var quantity = 5;

            // Act
            var exception = Record.Exception(() => new SummUpProduct(id, null!, quantity));

            // Assert
            exception.Should().NotBeNull().And.BeOfType<ArgumentNullException>(nameof(Product));
        }

        [Fact(DisplayName = $"{nameof(SummUpProduct)} can not be created with uncorrect quantity")]
        [Trait("Category", "Unit")]
        public void CanNotBeCreatedBecauseQuantityUncorrect()
        {
            // Arrange
            var id = 10;
            var quantity = -1;
            var deviceType = new Mock<DeviceType>(1).Object;
            var producer = new Mock<Producer>().Object;

            var product = new Mock<Product>(1, deviceType, producer).Object;

            // Act
            var exception = Record.Exception(() => new SummUpProduct(id, product, quantity));

            // Assert
            exception.Should().NotBeNull().And.BeOfType<ArgumentException>();
        }

        [Fact(DisplayName = $"{nameof(SummUpProduct)} (s) can be compared correctly with false result")]
        [Trait("Category", "Unit")]
        public void SummUpProductsCanBeComparedCorrectlyWithFalse()
        {
            // Arrange
            var id1 = 11;
            var id2 = 11;
            var quantity1 = 5;
            var quantity2 = 5;
            var deviceType = new Mock<DeviceType>(1).Object;
            var producer = new Mock<Producer>().Object;

            var product1 = new Mock<Product>(1, deviceType, producer).Object;
            product1.Cost = 100;

            var product2 = new Mock<Product>(1, deviceType, producer).Object;
            product2.Cost = 101;

            // Act
            var summUpProductMock1 = new Mock<SummUpProduct>(id1, product1, quantity1);
            var summUpProductMock2 = new Mock<SummUpProduct>(id2, product2, quantity2);

            // Assert
            product1.Equals(product2).Should().BeFalse();
        }

        [Fact(DisplayName = $"{nameof(SummUpProduct)} (s) can be compared correctly with true result")]
        [Trait("Category", "Unit")]
        public void SummUpProductsCanBeComparedCorrectlyWithTrue()
        {
            // Arrange
            var id1 = 11;
            var id2 = 12;
            var quantity1 = 5;
            var quantity2 = 5;
            var deviceType = new Mock<DeviceType>(1).Object;
            var producer = new Mock<Producer>().Object;

            var product1 = new Mock<Product>(1, deviceType, producer).Object;
            product1.Cost = 100;

            var product2 = new Mock<Product>(1, deviceType, producer).Object;
            product2.Cost = 100;

            // Act
            var summUpProductMock1 = new Mock<SummUpProduct>(id1, product1, quantity1);
            var summUpProductMock2 = new Mock<SummUpProduct>(id2, product2, quantity2);

            // Assert
            product1.Equals(product2).Should().BeTrue();
        }

        [Fact(DisplayName = $"{nameof(SummUpProduct)} can correctly calculate total price")]
        [Trait("Category", "Unit")]
        public void CanCalculateTotalPrice()
        {
            // Arrange
            var id = 11;
            var quantity = 5;
            var deviceType = new Mock<DeviceType>(1).Object;
            var producer = new Mock<Producer>().Object;
            var product = new Mock<Product>(1, deviceType, producer).Object;
            
            var summUpProductMock = new Mock<SummUpProduct>(id, product, quantity);

            var cost = 100;

            // Act
            var expectedResult = (int) summUpProductMock.Object.Product!.Cost * summUpProductMock.Object.Quantity;
            var result = (int) summUpProductMock.Object.CalculateSummUpPrice();

            // Assert
            result.Should().Be(expectedResult);
        }
    }
}
