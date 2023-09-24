using AuthLab.Security.Implementations;

namespace AuthLab.Tests;

public class Main
{
    [Fact]
    public void PasswordValidation_ContainsBoth()
    {
        //Arrange
        var validator = new PasswordValidator();
        var pass = "dsa,ff5";

        //Act
        var res = validator.ValidatePassword(pass);

        //Assert
        Assert.True(res);
    }

    [Fact]
    public void PasswordValidation_ContainsPunct()
    {
        //Arrange
        var validator = new PasswordValidator();
        var pass = "asdf!lksda";

        //Act
        var res = validator.ValidatePassword(pass);

        //Assert
        Assert.False(res);
    }

    [Fact]
    public void PasswordValidation_ContainsNum()
    {
        //Arrange
        var validator = new PasswordValidator();
        var pass = "asdfasdf1asdf";

        //Act
        var res = validator.ValidatePassword(pass);

        //Assert
        Assert.False(res);
    }

    [Fact]
    public void PasswordValidation_DoesntContain()
    {
        //Arrange
        var validator = new PasswordValidator();
        var pass = "sjklslkdjfgh";

        //Act
        var res = validator.ValidatePassword(pass);

        //Assert
        Assert.False(res);
    }
}