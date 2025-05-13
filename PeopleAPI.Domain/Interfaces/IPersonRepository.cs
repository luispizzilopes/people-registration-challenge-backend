using PeopleAPI.Domain.Entities;

namespace PeopleAPI.Domain.Interfaces; 

public interface IPersonRepository
{
    Task<Person?> GetPerson(Guid id);
    Task<IEnumerable<Person>> GetPersons();
    Task<bool> CreatePerson(Person person);
    bool UpdatePerson(Person person);
    bool DeletePerson(Person person);
    Task<bool> ExistsCpf(string cpf);
    Task<bool> ExistsCpf(string cpf, Guid id);
}
