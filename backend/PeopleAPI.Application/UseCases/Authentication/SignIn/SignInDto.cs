using System.ComponentModel.DataAnnotations;

namespace PeopleAPI.Application.UseCases.Authentication.SignIn;

public class SignInDto
{
    [EmailAddress]
    [Required]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty; 
}
