using PeopleAPI.Application.Mappings;
using PeopleAPI.Application.UseCases.Person.CreatePerson;
using PeopleAPI.Application.UseCases.Person.CreatePersonWithRequiredAddress;
using PeopleAPI.Application.UseCases.Person.UpdatePersonWithRequiredAddress;
using Mapster;
using PeopleAPI.Domain.Enums;

namespace PeopleAPI.Tests.Application.Mappings;

public class MapsterConfigurationTest
{
    public MapsterConfigurationTest()
    {
        MapsterConfiguration.RegisterMappings();
    }

    [Fact]
    public void CreatePersonDto_WhenMapped_ShouldMapToPersonCorrectly()
    {
        var dto = new CreatePersonDto
        {
            Name = "João",
            BirthDate = new DateTime(1990, 1, 1),
            Cpf = "11144477735", 
            Address = "Rua Exemplo, 123",
            Gender = Gender.Male,
            Email = "joao@email.com",
            Naturality = "SP",
            Nacionality = "Brasileiro"
        };

        var person = dto.Adapt<PeopleAPI.Domain.Entities.Person>();

        Assert.Equal(dto.Name, person.Name);
        Assert.Equal(dto.BirthDate, person.BirthDate);
        Assert.Equal(dto.Cpf, person.Cpf);
        Assert.Equal(dto.Address, person.Address);
        Assert.Equal(dto.Gender, person.Gender);
        Assert.Equal(dto.Email, person.Email);
        Assert.Equal(dto.Naturality, person.Naturality);
        Assert.Equal(dto.Nacionality, person.Nacionality);
    }

    [Fact]
    public void CreatePersonWithRequiredAddressDto_WhenMapped_ShouldMapToPersonCorrectly()
    {
        var dto = new CreatePersonWithRequiredAddressDto
        {
            Name = "Maria",
            BirthDate = new DateTime(1985, 5, 15),
            Cpf = "98765432100", // CPF válido
            Address = "Av. Central, 456",
            Gender = Gender.Female,
            Email = "maria@email.com",
            Naturality = "RJ",
            Nacionality = "Brasileira"
        };

        var person = dto.Adapt<PeopleAPI.Domain.Entities.Person>();

        Assert.Equal(dto.Name, person.Name);
        Assert.Equal(dto.BirthDate, person.BirthDate);
        Assert.Equal(dto.Cpf, person.Cpf);
        Assert.Equal(dto.Address, person.Address);
        Assert.Equal(dto.Gender, person.Gender);
        Assert.Equal(dto.Email, person.Email);
        Assert.Equal(dto.Naturality, person.Naturality);
        Assert.Equal(dto.Nacionality, person.Nacionality);
    }

    [Fact]
    public void UpdatePersonWithRequiredAddressDto_WhenMapped_ShouldMapToPersonCorrectly()
    {
        var dto = new UpdatePersonWithRequiredAddressDto
        {
            Name = "Carlos",
            BirthDate = new DateTime(2000, 12, 31),
            Cpf = "12345678909",
            Address = "Rua Nova, 789",
            Gender = Gender.Male,
            Email = "carlos@email.com",
            Naturality = "MG",
            Nacionality = "Brasileiro"
        };

        var person = dto.Adapt<PeopleAPI.Domain.Entities.Person>();

        Assert.Equal(dto.Name, person.Name);
        Assert.Equal(dto.BirthDate, person.BirthDate);
        Assert.Equal(dto.Cpf, person.Cpf);
        Assert.Equal(dto.Address, person.Address);
        Assert.Equal(dto.Gender, person.Gender);
        Assert.Equal(dto.Email, person.Email);
        Assert.Equal(dto.Naturality, person.Naturality);
        Assert.Equal(dto.Nacionality, person.Nacionality);
    }
}
