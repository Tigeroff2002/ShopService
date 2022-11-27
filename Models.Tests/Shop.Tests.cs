using Xunit;

using FluentAssertions;

using Moq;

using Models;

namespace Models.Tests
{
    public class ShopTests
    {
        [Fact(DisplayName = $"{nameof(Shop)} can be created")]
        [Trait("Category", "Unit")]
        public void CanBeCreated()
        {
            // Arrange
            var name = "name";
            var address = "address";
            var workingTime = "time";

            // Act
            var exception = Record.Exception(() => new Shop(name, address, workingTime));

            // Assert
            exception.Should().BeNull();
        }

        [Theory(DisplayName = $"{nameof(Shop)} can not be created because arguments are null or whitespace (empty)")]
        [Trait("Category", "Unit")]
        [InlineData(" ", "adress", "time")]
        [InlineData("name", "", "time")]
        [InlineData("name", "adress", null!)]
        public void CanBeCreatedBecauseStringArgumentsUncorrect(string name, string address, string workingTime)
        {
            // Act
            var exception = Record.Exception(() => new Shop(name, address, workingTime));

            // Assert
            exception.Should().NotBeNull().And.BeOfType<ArgumentException>();
        }
    }
}
