using PeopleAPI.Domain.Entities.Base;
using PeopleAPI.Domain.Exception;
using PeopleAPI.Domain.Exception.Messages;
using System.Text.RegularExpressions;

namespace PeopleAPI.Domain.Entities; 

public class User : BaseEntity
{
    public string Email { get; private set; } = string.Empty;
    public string Password { get; private set; } = string.Empty; 

    public User() { }

    public static User Create(string email, string password)
    {
        if (!ValidateEmail(email))
            throw new DomainException(UserMessagesException.EmailInvalid);

        if (!ValidatePassword(password))
            throw new DomainException(UserMessagesException.PasswordInvalid);


        return new User
        {
            Email = email,
            Password = password,
        }; 
    }

    public static bool ValidateEmail(string email) => Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
    public static bool ValidatePassword(string password) => Regex.IsMatch(password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$"); 
}
