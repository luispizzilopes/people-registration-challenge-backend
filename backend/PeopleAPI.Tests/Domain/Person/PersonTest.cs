using PeopleAPI.Domain.Enums;
using PeopleAPI.Domain.Exception;
using PeopleAPI.Domain.Exception.Messages;

namespace PeopleAPI.Tests.Domain.Person; 

public class PersonTest
{
    [Fact]
    public void CreatePerson_WithValidData_ShouldSucceed()
    {
        var name = "João da Silva";
        var birthDate = new DateTimeOffset(1990, 5, 15, 0, 0, 0, TimeSpan.Zero);
        var cpf = "48787696886";
        var email = "joao@email.com";

        var person = PeopleAPI.Domain.Entities.Person.Create(name, birthDate, cpf, "Rua A", Gender.Male, email, "Brasília", "Brasileiro");

        Assert.Equal(name, person.Name);
        Assert.Equal(birthDate, person.BirthDate);
        Assert.Equal(cpf, person.Cpf);
        Assert.Equal(email, person.Email);
    }

    [Fact]
    public void CreatePerson_WithInvalidCpf_ShouldThrowException()
    {
        var cpf = "11111111111";

        var ex = Assert.Throws<DomainException>(() =>
            PeopleAPI.Domain.Entities.Person.Create("Maria", DateTimeOffset.Now.AddYears(-20), cpf, null, null, null, null, null));

        Assert.Equal(PersonMessagesException.CpfInvalid, ex.Message);
    }

    [Fact]
    public void CreatePerson_WithEmptyName_ShouldThrowException()
    {
        var ex = Assert.Throws<DomainException>(() =>
            PeopleAPI.Domain.Entities.Person.Create("", DateTimeOffset.Now.AddYears(-20), "12345678909", null, null, null, null, null));

        Assert.Equal(PersonMessagesException.NameIsRequired, ex.Message);
    }

    [Fact]
    public void CreatePerson_WithEmptyCpf_ShouldThrowException()
    {
        var ex = Assert.Throws<DomainException>(() =>
            PeopleAPI.Domain.Entities.Person.Create("Carlos", DateTimeOffset.Now.AddYears(-20), "", null, null, null, null, null));

        Assert.Equal(PersonMessagesException.CpfIsRequired, ex.Message);
    }

    [Fact]
    public void CreatePerson_WithInvalidBirthDate_ShouldThrowException()
    {
        var today = DateTimeOffset.Now.Date;

        var ex = Assert.Throws<DomainException>(() =>
            PeopleAPI.Domain.Entities.Person.Create("Carlos", today, "12345678909", null, null, null, null, null));

        Assert.Equal(PersonMessagesException.BirthDateInvalid, ex.Message);
    }

    [Theory]
    [InlineData("test@example.com", true)]
    [InlineData("usuario@dominio.com", true)]
    [InlineData("email.invalido", false)]
    [InlineData("user@.com", false)]
    [InlineData("user@site", false)]
    public void ValidateEmail_ShouldReturnExpected(string email, bool expected)
    {
        var result = PeopleAPI.Domain.Entities.Person.ValidateEmail(email);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("12345678909", true)]
    [InlineData("11111111111", false)]
    [InlineData("123", false)]
    [InlineData("", false)]
    public void ValidateCpf_ShouldReturnExpected(string cpf, bool expected)
    {
        var result = PeopleAPI.Domain.Entities.Person.ValidateCpf(cpf);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void CreatePerson_WithOptionalFieldsNull_ShouldSucceed()
    {
        var person = PeopleAPI.Domain.Entities.Person.Create(
            "Lucas",
            DateTimeOffset.Now.AddYears(-30),
            "12345678909",
            null, null, null, null, null);

        Assert.Equal("Lucas", person.Name);
        Assert.Null(person.Email);
        Assert.Null(person.Address);
    }
}
