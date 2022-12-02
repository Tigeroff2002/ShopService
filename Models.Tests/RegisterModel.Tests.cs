using Xunit;

using FluentAssertions;

using Moq;

using AuthdService.Models;

namespace AuthdService.AuthModels.Tests
{
    public class RegisterModelsTests
    {
        [Fact(DisplayName = $"{nameof(RegisterModel)} can be created")]
        [Trait("Category", "Unit")]
        public void CanBeCreated()
        {
            // Arrange
            var email = "parahinkv@gmail.com";
            var password = "password";
            var confirmPassword = "password";
            var contactNumber = "";
            var isRoleChoosed = false;
            var roleType = 1;

            // Act
            var exception = Record.Exception(() => new RegisterModel(email, "", isRoleChoosed, roleType, password, confirmPassword, contactNumber));

            // Assert
            exception.Should().BeNull();
        }

        [Fact(DisplayName = $"{nameof(RegisterModel)} can not be created without email")]
        [Trait("Category", "Unit")]
        public void CanNotBeCreatedBecauseEmailEmpty()
        {
            // Arrange
            var password = "password";
            var confirmPassword = "password";
            var contactNumber = "";
            var isRoleChoosed = false;
            var roleType = 1;

            // Act
            var exception = Record.Exception(() => new RegisterModel("", "", isRoleChoosed, roleType, password, confirmPassword, contactNumber));

            // Assert
            exception.Should().NotBeNull().And.BeOfType<ArgumentException>(nameof(String));
        }

        [Fact(DisplayName = $"{nameof(RegisterModel)} can not be created with different password and confirmPassword")]
        [Trait("Category", "Unit")]
        public void CanNotBeCreatedBecausePasswordDoesNotEquals()
        {
            // Arrange
            var email = "parahinkv@gmail.com";
            var password = "password1";
            var confirmPassword = "password2";
            var contactNumber = "";
            var isRoleChoosed = false;
            var roleType = 1;

            // Act
            var exception = Record.Exception(() => new RegisterModel(email, "", isRoleChoosed, roleType, password, confirmPassword, contactNumber));

            // Assert
            exception.Should().NotBeNull().And.BeOfType<ArgumentException>();
        }
    }
}