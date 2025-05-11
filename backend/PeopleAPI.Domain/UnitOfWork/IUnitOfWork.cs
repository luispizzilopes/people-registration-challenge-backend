using PeopleAPI.Domain.Interfaces;

namespace PeopleAPI.Domain.UnitOfWork; 

public interface IUnitOfWork
{
    IPersonRepository PersonRepository { get; }
    IUserRepository UserRepository { get; }

    Task Commit(); 
}
