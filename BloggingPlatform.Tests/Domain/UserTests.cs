using BloggingPlatform.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BloggingPlatform.Tests.Domain
{
    public class UserTests
    {
        [Fact]
        public void NewUser_WithValidParameters_ShouldCreateUserAndAssignProperties()
        {
            // Arrange
            var expectedUsername = "ernesto.dev";
            var expectedEmail = "ernesto@test.com";

            // Act
            var user = new User(expectedUsername, expectedEmail);

            // Assert
            Assert.NotNull(user);
            Assert.Equal(expectedUsername, user.Username); 
            Assert.Equal(expectedEmail, user.Email);
            Assert.NotEqual(Guid.Empty, user.Id); 
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void NewUser_WhenUserNameIsNullOrEmptyOrWhitespace_ShouldThrowArgumentException(string? usernameInvalido) 
        {
            //Arrange
            var emailValido = "test@test.com";

            //Act
            var exception = Assert.Throws<ArgumentException>(() => new User(usernameInvalido, emailValido));

            //Assert
            Assert.Contains("El nombre de usuario no puede estar vacío.", exception.Message);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("sinarroba.com")]
        public void NewUser_WhenEmailIsNullOrEmptyOrWhitespaceOrInvalid_ShouldThrowArgumentException(string? emailInvalido)
        {
            //Arrange
            var usernameValido = "test";

            //Act
            var exception = Assert.Throws<ArgumentException>(() => new User(usernameValido, emailInvalido));

            //Assert
            Assert.Contains("El formato del correo electrónico no es válido.", exception.Message);
        }
    }
}
