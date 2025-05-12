using PeopleAPI.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace PeopleAPI.Application.UseCases.Person.CreatePersonWithRequiredAddress;

public class CreatePersonWithRequiredAddressDto
{
    [Required(ErrorMessage = "O campo Nome é obrigatório.")]
    [MaxLength(200, ErrorMessage = "O campo Nome deve ter no máximo 200 caracteres.")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo Data de Nascimento é obrigatório.")]
    public DateTimeOffset BirthDate { get; set; }

    [Required(ErrorMessage = "O campo CPF é obrigatório.")]
    [MaxLength(11, ErrorMessage = "O campo CPF deve ter no máximo 11 caracteres.")]
    public string Cpf { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo Endereço é obrigatório.")]
    [MaxLength(200, ErrorMessage = "O campo Endereço deve ter no máximo 200 caracteres.")]
    public string? Address { get; set; }

    public Gender? Gender { get; set; }

    [MaxLength(200, ErrorMessage = "O campo Email deve ter no máximo 200 caracteres.")]
    public string? Email { get; set; }

    [MaxLength(100, ErrorMessage = "O campo Naturalidade deve ter no máximo 100 caracteres.")]
    public string? Naturality { get; set; }

    [MaxLength(100, ErrorMessage = "O campo Nacionalidade deve ter no máximo 100 caracteres.")]
    public string? Nacionality { get; set; }
}

