using Xunit;

using FluentAssertions;

using Moq;

using ShopService.Views.Account;

namespace AuthdService.AuthModels.Tests
{
    public class LoginModelTests
    {
        [Fact(DisplayName = $"{nameof(LoginModel)} can be created")]
        [Trait("Category", "Unit")]
        public void CanBeCreated()
        {
            // Arrange

            // Act
            var exception = Record.Exception(() => new LoginModel());

            // Assert
            exception.Should().BeNull();
        }
    }
}
