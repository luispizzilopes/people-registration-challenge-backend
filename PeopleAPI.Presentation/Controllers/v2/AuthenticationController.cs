using Microsoft.AspNetCore.Mvc;
using PeopleAPI.Application.Facades.Authentication;
using PeopleAPI.Application.Services.TokenJwt;
using PeopleAPI.Application.UseCases.Authentication.SignIn;
using PeopleAPI.Shared.Common;

namespace PeopleAPI.Presentation.Controllers.v2;

/// <summary>
/// Controlador responsável pelas operações de autenticação de usuários.
/// </summary>
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("2.0")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationFacade _authenticationFacade;

    /// <summary>
    /// Construtor do controlador de autenticação.
    /// </summary>
    /// <param name="authenticationFacade">Fachada de autenticação para lidar com os casos de uso.</param>
    public AuthenticationController(IAuthenticationFacade authenticationFacade)
    {
        _authenticationFacade = authenticationFacade;
    }

    /// <summary>
    /// Realiza a autenticação do usuário e retorna um token JWT caso as credenciais estejam corretas.
    /// </summary>
    /// <param name="signIn">Dados do usuário para autenticação (e-mail e senha).</param>
    /// <returns>Um token JWT em caso de sucesso ou uma resposta de erro em caso de falha.</returns>
    [HttpPost("sign-in")]
    public async Task<ActionResult<ResultWithValue<TokenDto>>> SignIn([FromBody] SignInDto signIn)
    {
        var response = await _authenticationFacade.SignIn(signIn);
        return response.IsSuccess ? Ok(response) : BadRequest(response);
    }
}
