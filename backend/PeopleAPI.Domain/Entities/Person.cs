using PeopleAPI.Domain.Entities.Base;
using PeopleAPI.Domain.Enums;
using PeopleAPI.Domain.Exception;
using PeopleAPI.Domain.Exception.Messages;
using System.Text.RegularExpressions;

namespace PeopleAPI.Domain.Entities; 

public class Person : BaseEntity
{
    public string Name { get; set; } = string.Empty; 
    public DateTimeOffset BirthDate { get; set; }
    public string Cpf { get; set; } = string.Empty; 
    public string? Address { get; set; }
    public Gender? Gender { get; set; }
    public string? Email { get; set; }
    public string? Naturality { get; set; }
    public string? Nacionality { get; set; }

    public Person() { }

    public static Person Create(string name,
        DateTimeOffset birthDate,
        string cpf,
        string? address,
        Gender? gender,
        string? email,
        string? naturality,
        string? nacionality)
    {
        if (string.IsNullOrEmpty(cpf))
            throw new DomainException(PersonMessagesException.CpfIsRequired); 

        if (!ValidateCpf(cpf))
            throw new DomainException(PersonMessagesException.CpfInvalid);

        if (string.IsNullOrEmpty(name))
            throw new DomainException(PersonMessagesException.NameIsRequired);

        if (birthDate == null)
            throw new DomainException(PersonMessagesException.BirthDateIsRequired); 

        if(!ValidateBirthDate(birthDate))
            throw new DomainException(PersonMessagesException.BirthDateInvalid);

        return new Person
        {
            Name = name,
            BirthDate = birthDate,
            Cpf = cpf,
            Address = address,
            Gender = gender,
            Email = email,
            Naturality = naturality,
            Nacionality = nacionality,
        }; 
    }

    public static bool ValidateCpf(string cpf)
    {
        if (string.IsNullOrWhiteSpace(cpf))
            return false;

        cpf = new string(cpf.Where(char.IsDigit).ToArray());

        if (cpf.Length != 11 || cpf.Distinct().Count() == 1)
            return false;

        return CheckDigits(cpf);
    }

     static bool CheckDigits(ReadOnlySpan<char> cpf)
    {
        var firstDigit = CalculateDigit(cpf, 9, 10);
        var secondDigit = CalculateDigit(cpf, 10, 11);

        return cpf[9] - '0' == firstDigit && cpf[10] - '0' == secondDigit;
    }

     static int CalculateDigit(ReadOnlySpan<char> cpf, int length, int weightStart)
    {
        int sum = 0;
        for (int i = 0; i < length; i++)
            sum += (cpf[i] - '0') * (weightStart - i);

        int remainder = sum % 11;
        return remainder < 2 ? 0 : 11 - remainder;
    }

    public static bool ValidateEmail(string email) => Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");

    public static bool ValidateBirthDate(DateTimeOffset birthDate)
    {
        if (birthDate >= DateTimeOffset.Now.Date)
            return false;

        return true; 
    }
}
