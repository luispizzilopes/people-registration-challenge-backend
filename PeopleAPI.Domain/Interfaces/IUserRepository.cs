using PeopleAPI.Domain.Entities;

namespace PeopleAPI.Domain.Interfaces; 

public interface IUserRepository
{
    Task<User?> FindUser(string email, string password);
    Task<User?> GetUser(Guid id);
    Task<User?> GetUserByEmail(string email);
    Task<IEnumerable<User>> GetUsers();
    Task<bool> CreateUser(User user);
    bool UpdateUser(User user);
    bool DeleteUser(User user);
}
