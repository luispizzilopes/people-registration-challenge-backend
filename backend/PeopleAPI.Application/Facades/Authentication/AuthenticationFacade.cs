using PeopleAPI.Application.Services.TokenJwt;
using PeopleAPI.Application.UseCases.Authentication.SignIn;
using PeopleAPI.Shared.Common;

namespace PeopleAPI.Application.Facades.Authentication;

public class AuthenticationFacade : IAuthenticationFacade
{
    private readonly SignInUseCase _signInUseCase;

    public AuthenticationFacade(SignInUseCase signInUseCase)
    {
        _signInUseCase = signInUseCase;
    }

    public async Task<ResultWithValue<TokenDto>> SignIn(SignInDto signIn) =>
        await _signInUseCase.ExecuteAsync(signIn);
}
