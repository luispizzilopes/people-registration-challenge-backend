using PeopleAPI.Domain.Entities;

namespace PeopleAPI.Domain.Interfaces; 

public interface IUserRepository
{
    Task<User> GetUser(Guid id);
    Task<IEnumerable<User>> GetUsers();
    Task<User> CreateUser(User User);
    Task<User> UpdateUser(User User);
    Task<bool> DeleteUser(User User);
}
