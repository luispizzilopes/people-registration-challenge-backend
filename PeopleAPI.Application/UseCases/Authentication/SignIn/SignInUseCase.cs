using PeopleAPI.Application.Services.TokenJwt;
using PeopleAPI.Domain.UnitOfWork;
using PeopleAPI.Shared.Common;

namespace PeopleAPI.Application.UseCases.Authentication.SignIn; 

public class SignInUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITokenJwtService _tokenJwtService;

    public SignInUseCase(IUnitOfWork unitOfWork, ITokenJwtService tokenJwtService)
    {
        _unitOfWork = unitOfWork;
        _tokenJwtService = tokenJwtService;
    }

    public async Task<ResultWithValue<TokenDto>> ExecuteAsync(SignInDto signIn)
    {
        var user = await _unitOfWork.UserRepository
            .FindUser(signIn.Email, signIn.Password);

        if (user is null)
            return ResultWithValue<TokenDto>.Failure("As credenciais informadas são inválidas!"); 

        return ResultWithValue<TokenDto>.Success(_tokenJwtService.CreateTokenUser(user), "Autenticação realizada com sucesso!");
    }
}
