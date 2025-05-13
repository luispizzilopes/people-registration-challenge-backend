using Mapster;
using PeopleAPI.Domain.UnitOfWork;
using PeopleAPI.Shared.Common;

namespace PeopleAPI.Application.UseCases.User.CreateUser;

public class CreateUserUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateUserUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> ExecuteAsync(CreateUserDto createUser)
    {
        var validationResult = await ValidateEmailUser(createUser.Email);
        if (!validationResult.IsSuccess)
            return validationResult;

        var userEntity = createUser.Adapt<Domain.Entities.User>();
        bool created = await _unitOfWork.UserRepository.CreateUser(userEntity);

        if (!created)
            return Result.Failure("Não foi possível criar o usuário!");

        await _unitOfWork.Commit();
        return Result.Success("Usuário criado com sucesso!");
    }

    private async Task<Result> ValidateEmailUser(string email)
    {
        var user = await _unitOfWork.UserRepository.GetUserByEmail(email);

        return user is not null ?
            Result.Failure("O Email informado já existe na base de dados!") :
            Result.Success(string.Empty);
    }
}
