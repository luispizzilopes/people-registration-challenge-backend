using PeopleAPI.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace PeopleAPI.Application.UseCases.Person.CreatePerson
{
    public class CreatePersonDto
    {
        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public DateTimeOffset BirthDate { get; set; }

        [Required]
        [MaxLength(11)]
        public string Cpf { get; set; } = string.Empty;

        [MaxLength(200)]
        public string? Address { get; set; }

        public Gender? Gender { get; set; }

        [MaxLength(200)]
        public string? Email { get; set; }

        [MaxLength(100)]
        public string? Naturality { get; set; }

        [MaxLength(100)]
        public string? Nacionality { get; set; }
    }
}
