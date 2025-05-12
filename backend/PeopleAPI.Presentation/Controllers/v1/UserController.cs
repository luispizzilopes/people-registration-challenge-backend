using Microsoft.AspNetCore.Mvc;
using PeopleAPI.Application.Facades.User;
using PeopleAPI.Application.UseCases.User.CreateUser;
using PeopleAPI.Shared.Common;

namespace PeopleAPI.Presentation.Controllers.v1;

/// <summary>
/// Controlador responsável pelas operações relacionadas ao usuário.
/// </summary>
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserFacade _userFacade;

    /// <summary>
    /// Construtor do controlador de usuários.
    /// </summary>
    /// <param name="userFacade">Fachada de usuário para lidar com os casos de uso.</param>
    public UserController(IUserFacade userFacade)
    {
        _userFacade = userFacade;
    }

    /// <summary>
    /// Cria um novo usuário no sistema.
    /// </summary>
    /// <param name="createUser">Dados do usuário a ser criado.</param>
    /// <returns>Resultado da operação de criação do usuário.</returns>
    [HttpPost]
    public async Task<ActionResult<Result>> CreateUser([FromBody] CreateUserDto createUser)
    {
        var response = await _userFacade.CreateUser(createUser);
        return response.IsSuccess ? Ok(response) : BadRequest(response);
    }
}
