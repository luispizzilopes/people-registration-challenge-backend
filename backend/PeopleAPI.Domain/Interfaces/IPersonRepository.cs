using PeopleAPI.Domain.Entities;

namespace PeopleAPI.Domain.Interfaces; 

public interface IPersonRepository
{
    Task<Person> GetPerson(Guid id);
    Task<IEnumerable<Person>> GetPersons();
    Task<Person> CreatePerson(Person person);
    Task<Person> UpdatePerson(Person person);
    Task<bool> DeletePerson(Person person); 
}
