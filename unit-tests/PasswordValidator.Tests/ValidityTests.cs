using System;
using Xunit;
using Validators.Password;

namespace PasswordValidatorTests
{
  public class ValidityTest
  {
    [Fact]
    public void ValidPassword()
    {
      //Arrange
      var passwordValidator = new PasswordValidator();
      const string password = "Th1sIsapassword!";

      //Act
      bool isValid = passwordValidator.IsValid(password);

      //Assert
      Assert.True(isValid, $"The password {password} is not valid");
    }

    [Fact]
    public void NotValidPassword()
    {
      //Arrange
      var passwordValidator = new PasswordValidator();
      const string password = "thisIsaPassword";

      //Act
      bool isValid = passwordValidator.IsValid(password);

      //Assert
      Assert.False(isValid, $"The password {password} should not be valid!");
    }

    [Theory]
    [InlineData("Th1sIsapassword!", true)]
    [InlineData("thisIsapassword123!", true)]
    [InlineData("Abc$123456", true)]
    [InlineData("Th1s!", false)]
    [InlineData("thisIsAPassword", false)]
    [InlineData("thisisapassword#", false)]
    [InlineData("THISISAPASSWORD123!", false)]
    [InlineData("", false)]
    public void ValidatePassword(string password, bool expectedResult)
    {
      //Arrange
      var passwordValidator = new PasswordValidator();

      //Act
      bool isValid = passwordValidator.IsValid(password);
      
       //Assert
      Assert.Equal(expectedResult, isValid);
    }
  }
}