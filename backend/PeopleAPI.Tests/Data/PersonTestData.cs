using PeopleAPI.Domain.Entities;
using PeopleAPI.Domain.Enums;

namespace PeopleAPI.Tests.Data;

public static class PersonTestData
{
    public static PeopleAPI.Domain.Entities.Person GetValidPerson()
    {
        return new PersonBuilder()
            .WithName("João da Silva")
            .WithBirthDate(new DateTimeOffset(1990, 5, 10, 0, 0, 0, TimeSpan.Zero))
            .WithCpf("48787696886")
            .WithAddress("Rua A")
            .WithGender(Gender.Male)
            .WithEmail("joao@email.com")
            .WithNaturality("Brasília")
            .WithNacionality("Brasileiro")
            .Build();
    }
}

public class PersonBuilder
{
    private string _name = "Default Name";
    private DateTimeOffset _birthDate = DateTimeOffset.UtcNow;
    private string _cpf = "00000000000";
    private string? _address = null;
    private Gender? _gender = null;
    private string? _email = null;
    private string? _naturality = null;
    private string? _nacionality = null;

    public PersonBuilder WithName(string name) { _name = name; return this; }
    public PersonBuilder WithBirthDate(DateTimeOffset birthDate) { _birthDate = birthDate; return this; }
    public PersonBuilder WithCpf(string cpf) { _cpf = cpf; return this; }
    public PersonBuilder WithAddress(string address) { _address = address; return this; }
    public PersonBuilder WithGender(Gender gender) { _gender = gender; return this; }
    public PersonBuilder WithEmail(string email) { _email = email; return this; }
    public PersonBuilder WithNaturality(string naturality) { _naturality = naturality; return this; }
    public PersonBuilder WithNacionality(string nacionality) { _nacionality = nacionality; return this; }

    public PeopleAPI.Domain.Entities.Person Build()
    {
        var person = new PeopleAPI.Domain.Entities.Person();

        typeof(PeopleAPI.Domain.Entities.Person).GetProperty("Name")?.SetValue(person, _name);
        typeof(PeopleAPI.Domain.Entities.Person).GetProperty("BirthDate")?.SetValue(person, _birthDate);
        typeof(PeopleAPI.Domain.Entities.Person).GetProperty("Cpf")?.SetValue(person, _cpf);
        typeof(PeopleAPI.Domain.Entities.Person).GetProperty("Address")?.SetValue(person, _address);
        typeof(PeopleAPI.Domain.Entities.Person).GetProperty("Gender")?.SetValue(person, _gender);
        typeof(PeopleAPI.Domain.Entities.Person).GetProperty("Email")?.SetValue(person, _email);
        typeof(PeopleAPI.Domain.Entities.Person).GetProperty("Naturality")?.SetValue(person, _naturality);
        typeof(PeopleAPI.Domain.Entities.Person).GetProperty("Nacionality")?.SetValue(person, _nacionality);

        return person;
    }
}
