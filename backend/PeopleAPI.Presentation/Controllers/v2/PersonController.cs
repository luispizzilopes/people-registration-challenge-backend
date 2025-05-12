using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PeopleAPI.Application.Facades.Person;
using PeopleAPI.Application.UseCases.Person.CreatePersonWithRequiredAddress;
using PeopleAPI.Application.UseCases.Person.UpdatePersonWithRequiredAddress;
using PeopleAPI.Shared.Common;

namespace PeopleAPI.Presentation.Controllers.v2;

/// <summary>
/// Controlador responsável pelas operações de pessoa com endereço obrigatório (versão 2 da API).
/// </summary>
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("2.0")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class PersonController : ControllerBase
{
    private readonly IPersonFacade _personFacade;

    /// <summary>
    /// Construtor do controlador de pessoas da versão 2.
    /// </summary>
    /// <param name="personFacade">Fachada de pessoa que contém os casos de uso relacionados à versão 2.</param>
    public PersonController(IPersonFacade personFacade)
    {
        _personFacade = personFacade;
    }

    /// <summary>
    /// Cria uma nova pessoa no sistema com endereço obrigatório.
    /// </summary>
    /// <param name="createPersonWithRequiredAddress">Dados da pessoa e do endereço a serem cadastrados.</param>
    /// <returns>Resultado da operação de criação.</returns>
    [HttpPost]
    public async Task<ActionResult<Result>> CreatePerson([FromBody] CreatePersonWithRequiredAddressDto createPersonWithRequiredAddress)
    {
        var response = await _personFacade.CreatePersonWithRequiredAddressUseCase(createPersonWithRequiredAddress);
        return response.IsSuccess ? Ok(response) : BadRequest(response);
    }

    /// <summary>
    /// Atualiza os dados de uma pessoa com endereço obrigatório.
    /// </summary>
    /// <param name="updatePersonWithRequiredAddress">Dados atualizados da pessoa e do endereço.</param>
    /// <returns>Resultado da operação de atualização.</returns>
    [HttpPut]
    public async Task<ActionResult<Result>> UpdatePerson([FromBody] UpdatePersonWithRequiredAddressDto updatePersonWithRequiredAddress)
    {
        var response = await _personFacade.UpdatePersonWithRequiredAddressUseCase(updatePersonWithRequiredAddress);
        return response.IsSuccess ? Ok(response) : BadRequest(response);
    }
}
