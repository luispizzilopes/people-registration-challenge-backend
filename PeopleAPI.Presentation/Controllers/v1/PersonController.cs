using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PeopleAPI.Application.Facades.Person;
using PeopleAPI.Application.UseCases.Person.CreatePerson;
using PeopleAPI.Shared.Common;

namespace PeopleAPI.Presentation.Controllers.v1;

/// <summary>
/// Controlador responsável pelas operações relacionadas a pessoas.
/// </summary>
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class PersonController : ControllerBase
{
    private readonly IPersonFacade _personFacade;

    /// <summary>
    /// Construtor do controlador de pessoas.
    /// </summary>
    /// <param name="personFacade">Fachada de pessoa para lidar com os casos de uso.</param>
    public PersonController(IPersonFacade personFacade)
    {
        _personFacade = personFacade;
    }

    /// <summary>
    /// Retorna a lista de todas as pessoas cadastradas.
    /// </summary>
    /// <returns>Lista de pessoas.</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Application.UseCases.Person.GetPersons.PersonDto>>> GetPersons()
    {
        return Ok(await _personFacade.GetPersonsUseCase());
    }

    /// <summary>
    /// Retorna os dados de uma pessoa específica pelo identificador.
    /// </summary>
    /// <param name="id">Identificador da pessoa.</param>
    /// <returns>Dados da pessoa encontrada ou erro caso não exista.</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<ResultWithValue<Application.UseCases.Person.GetPerson.PersonDto>>> GetPerson([FromRoute] Guid id)
    {
        var response = await _personFacade.GetPersonUseCase(id);
        return response.IsSuccess ? Ok(response) : BadRequest(response);
    }

    /// <summary>
    /// Cria uma nova pessoa no sistema.
    /// </summary>
    /// <param name="createPerson">Dados da pessoa a ser criada.</param>
    /// <returns>Resultado da operação de criação.</returns>
    [HttpPost]
    public async Task<ActionResult<Result>> CreatePerson([FromBody] CreatePersonDto createPerson)
    {
        var response = await _personFacade.CreatePersonUseCase(createPerson);
        return response.IsSuccess ? Ok(response) : BadRequest(response);
    }

    /// <summary>
    /// Atualiza os dados de uma pessoa existente.
    /// </summary>
    /// <param name="updatePerson">Dados da pessoa a serem atualizados.</param>
    /// <returns>Resultado da operação de atualização.</returns>
    [HttpPut]
    public async Task<ActionResult<Result>> UpdatePerson([FromBody] UpdatePersonDto updatePerson)
    {
        var response = await _personFacade.UpdatePersonUseCase(updatePerson);
        return response.IsSuccess ? Ok(response) : BadRequest(response);
    }

    /// <summary>
    /// Remove uma pessoa do sistema com base no identificador.
    /// </summary>
    /// <param name="id">Identificador da pessoa a ser removida.</param>
    /// <returns>Resultado da operação de exclusão.</returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult<Result>> DeletePerson([FromRoute] Guid id)
    {
        var response = await _personFacade.DeletePersonUseCase(id);
        return response.IsSuccess ? Ok(response) : BadRequest(response);
    }
}
