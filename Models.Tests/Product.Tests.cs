using Xunit;

using FluentAssertions;

using Moq;

using Models;

namespace Models.Tests
{
    public class ProductTests
    {
        [Fact(DisplayName = $"{nameof(Product)} can be created")]
        [Trait("Category", "Unit")]
        public void CanBeCreated()
        {
            // Arrange
            var id = 1;
            var producer = Mock.Of<Producer>();
            var deviceType = new Mock<DeviceType>(1).Object;

            // Act
            var exception = Record.Exception(() => new Product(id, deviceType, producer));

            // Assert
            exception.Should().BeNull();
        }

        [Theory(DisplayName = $"{nameof(Product)} can not be created cause id is uncorrect")]
        [Trait("Category", "Unit")]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-10)]
        public void CanNotBeCreatedCauseIdUncorrect(int id)
        {
            // Arrange
            var producer = Mock.Of<Producer>();
            var deviceType = new Mock<DeviceType>(1).Object;

            // Act
            var exception = Record.Exception(() => new Product(id, deviceType, producer));

            // Assert
            exception.Should().NotBeNull().And.BeOfType<ArgumentException>();
        }

        [Fact(DisplayName = $"{nameof(Product)} can not be created cause producer is null")]
        [Trait("Category", "Unit")]
        public void CanNotBeCreatedCauseProducerNull()
        {
            // Arrange
            var id = 1;
            var deviceType = new Mock<DeviceType>(1).Object;

            // Act
            var exception = Record.Exception(() => new Product(id, deviceType, null!));

            // Assert
            exception.Should().NotBeNull().And.BeOfType<ArgumentNullException>();
        }

        [Fact(DisplayName = $"{nameof(Product)} can not be created cause devicetype is null")]
        [Trait("Category", "Unit")]
        public void CanNotBeCreatedCauseDevicetypeNull()
        {
            // Arrange
            var id = 1;
            var producer = Mock.Of<Producer>();

            // Act
            var exception = Record.Exception(() => new Product(id, null!, producer));

            // Assert
            exception.Should().NotBeNull().And.BeOfType<ArgumentNullException>();
        }

        [Fact(DisplayName = $"{nameof(Product)} (s) can be compared correctly with false result")]
        [Trait("Category", "Unit")]
        public void ProductsCanBeComparedCorrectlyWithFalse()
        {
            // Arrange
            var id1 = 1;
            var id2 = 2;
            var producerMock = new Mock<Producer>();
            var deviceTypeMock = new Mock<DeviceType>(1);

            // Act
            var product1 = new Product(id1, deviceTypeMock.Object, producerMock.Object);
            var product2 = new Product(id2, deviceTypeMock.Object, producerMock.Object);

            // Assert
            product1.Equals(product2).Should().BeFalse();
        }

        [Fact(DisplayName = $"{nameof(Product)} (s) can be compared correctly with true result")]
        [Trait("Category", "Unit")]
        public void ProductsCanBeComparedCorrectlyWithTrue()
        {
            // Arrange
            var id1 = 1;
            var id2 = 1;
            var producerMock = new Mock<Producer>();

            var deviceTypeMock1 = new Mock<DeviceType>(1);
            var deviceTypeMock2 = new Mock<DeviceType>(2);

            // Act
            var product1 = new Product(id1, deviceTypeMock1.Object, producerMock.Object);
            var product2 = new Product(id2, deviceTypeMock2.Object, producerMock.Object);

            // Assert
            product1.Equals(product2).Should().BeTrue();
        }
    }
}
