using PeopleAPI.Application.Services.TokenJwt;
using PeopleAPI.Application.UseCases.Authentication.SignIn;
using PeopleAPI.Shared.Common;

namespace PeopleAPI.Application.Facades.Authentication; 

public interface IAuthenticationFacade
{
    Task<ResultWithValue<TokenDto>> SignIn(SignInDto signIn);
}
