using PeopleAPI.Domain.Entities;
using PeopleAPI.Domain.Exception.Messages;
using PeopleAPI.Domain.Exception;

namespace PeopleAPI.Tests.Domain.Person; 

public class UserTest
{
    [Fact]
    public void CreateUser_WithValidData_ShouldSucceed()
    {
        var email = "usuario@email.com";
        var password = "Senha123";

        var user = User.Create(email, password);

        Assert.Equal(email, user.Email);
        Assert.Equal(password, user.Password);
    }

    [Theory]
    [InlineData("emailinvalido")]
    [InlineData("usuario@")]
    [InlineData("usuario.com")]
    [InlineData("")]
    [InlineData("   ")]
    public void CreateUser_WithInvalidEmail_ShouldThrowException(string email)
    {
        var ex = Assert.Throws<DomainException>(() =>
            User.Create(email, "Senha123"));

        Assert.Equal(UserMessagesException.EmailInvalid, ex.Message);
    }

    [Theory]
    [InlineData("12345678")]
    [InlineData("senha123")]
    [InlineData("SENHA123")]
    [InlineData("Senha")]
    [InlineData("")]
    public void CreateUser_WithInvalidPassword_ShouldThrowException(string password)
    {
        var ex = Assert.Throws<DomainException>(() =>
            User.Create("usuario@email.com", password));

        Assert.Equal(UserMessagesException.PasswordInvalid, ex.Message);
    }

    [Theory]
    [InlineData("usuario@email.com", true)]
    [InlineData("teste@dominio.com", true)]
    [InlineData("invalido@", false)]
    [InlineData("semarroba.com", false)]
    [InlineData("", false)]
    public void ValidateEmail_ShouldReturnExpected(string email, bool expected)
    {
        var result = User.ValidateEmail(email);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("Senha123", true)]
    [InlineData("Abcdefg1", true)]
    [InlineData("abc123", false)]
    [InlineData("SENHA123", false)]
    [InlineData("senha123", false)]
    [InlineData("Senha", false)]
    public void ValidatePassword_ShouldReturnExpected(string password, bool expected)
    {
        var result = User.ValidatePassword(password);
        Assert.Equal(expected, result);
    }
}
