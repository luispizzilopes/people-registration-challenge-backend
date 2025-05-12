using System.ComponentModel.DataAnnotations;

namespace PeopleAPI.Application.UseCases.User.CreateUser; 

public class CreateUserDto
{
    [EmailAddress]
    [Required]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;
}
