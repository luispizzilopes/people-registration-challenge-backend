using PeopleAPI.Application.UseCases.User.CreateUser;
using PeopleAPI.Shared.Common;

namespace PeopleAPI.Application.Facades.User;

public interface IUserFacade
{
    Task<Result> CreateUser(CreateUserDto createUser);  
}
