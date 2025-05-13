using PeopleAPI.Application.UseCases.User.CreateUser;
using PeopleAPI.Shared.Common;

namespace PeopleAPI.Application.Facades.User; 

public class UserFacade : IUserFacade
{
    private readonly CreateUserUseCase _createUserUseCase;

    public UserFacade(CreateUserUseCase createUserUseCase)
    {
        _createUserUseCase = createUserUseCase;
    }

    public async Task<Result> CreateUser(CreateUserDto createUser) =>
        await _createUserUseCase.ExecuteAsync(createUser);
}
