namespace PeopleAPI.Domain.Exception; 

public class DomainException : ArgumentException
{
    public DomainException(string message) : base(message) { }
}
