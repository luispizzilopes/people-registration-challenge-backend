using PeopleAPI.Domain.Interfaces;

namespace PeopleAPI.Domain.UnitOfWork; 

public interface IUnitOfWork
{
    IPersonRepository PersonRepository { get; set; }
    IUserRepository UserRepository { get; set; }

    Task Commit(); 
}
