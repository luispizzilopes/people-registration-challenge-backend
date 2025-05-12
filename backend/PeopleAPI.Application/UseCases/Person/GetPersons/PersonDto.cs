using PeopleAPI.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace PeopleAPI.Application.UseCases.Person.GetPersons; 

public class PersonDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTimeOffset BirthDate { get; set; }
    public string Cpf { get; set; } = string.Empty;
    public string? Address { get; set; }
    public Gender? Gender { get; set; }
    public string? Email { get; set; }
    public string? Naturality { get; set; }
    public string? Nacionality { get; set; }
}
