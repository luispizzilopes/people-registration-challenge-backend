using System.ComponentModel.DataAnnotations;

namespace PeopleAPI.Application.UseCases.Authentication.SignIn;

public class SignInDto
{
    [Required(ErrorMessage = "O campo Email é obrigatório.")]
    [EmailAddress(ErrorMessage = "O campo Email deve conter um endereço de email válido.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo Senha é obrigatório.")]
    public string Password { get; set; } = string.Empty;
}
