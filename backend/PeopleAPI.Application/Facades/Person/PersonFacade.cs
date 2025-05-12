using PeopleAPI.Application.UseCases.Person.CreatePerson;
using PeopleAPI.Application.UseCases.Person.CreatePersonWithRequiredAddress;
using PeopleAPI.Application.UseCases.Person.DeletePerson;
using PeopleAPI.Application.UseCases.Person.GetPerson;
using PeopleAPI.Application.UseCases.Person.GetPersons;
using PeopleAPI.Application.UseCases.Person.UpdatePerson;
using PeopleAPI.Application.UseCases.Person.UpdatePersonWithRequiredAddress;
using PeopleAPI.Shared.Common;

namespace PeopleAPI.Application.Facades.Person;

public class PersonFacade : IPersonFacade
{
    private readonly CreatePersonUseCase _createPersonUseCase;
    private readonly CreatePersonWithRequiredAddressUseCase _createPersonWithRequiredAddressUseCase;
    private readonly UpdatePersonUseCase _updatePersonUseCase;
    private readonly UpdatePersonWithRequiredAddressUseCase _updatePersonWithRequiredAddressUseCase;
    private readonly GetPersonsUseCase _getPersonsUseCase;
    private readonly GetPersonUseCase _getPersonUseCase; 
    private readonly DeletePersonUseCase _deletePersonUseCase;

    public PersonFacade(
        CreatePersonUseCase createPersonUseCase, 
        CreatePersonWithRequiredAddressUseCase createPersonWithRequiredAddressUseCase, 
        UpdatePersonUseCase updatePersonUseCase, 
        UpdatePersonWithRequiredAddressUseCase updatePersonWithRequiredAddressUseCase, 
        GetPersonsUseCase getPersonsUseCase, 
        GetPersonUseCase getPersonUseCase, 
        DeletePersonUseCase deletePersonUseCase)
    {
        _createPersonUseCase = createPersonUseCase;
        _createPersonWithRequiredAddressUseCase = createPersonWithRequiredAddressUseCase;
        _updatePersonUseCase = updatePersonUseCase;
        _updatePersonWithRequiredAddressUseCase = updatePersonWithRequiredAddressUseCase;
        _getPersonsUseCase = getPersonsUseCase;
        _getPersonUseCase = getPersonUseCase;
        _deletePersonUseCase = deletePersonUseCase;
    }

    public async Task<IEnumerable<UseCases.Person.GetPersons.PersonDto>> GetPersonsUseCase() =>
        await _getPersonsUseCase.ExecuteAsync();

    public async Task<ResultWithValue<UseCases.Person.GetPerson.PersonDto>> GetPersonUseCase(Guid id) =>
        await _getPersonUseCase.ExecuteAsync(id);

    public async Task<Result> CreatePersonUseCase(CreatePersonDto createPerson) =>
        await _createPersonUseCase.ExecuteAsync(createPerson);

    public async Task<Result> CreatePersonWithRequiredAddressUseCase(CreatePersonWithRequiredAddressDto createPersonWithRequiredAddress) =>
        await _createPersonWithRequiredAddressUseCase.ExecuteAsync(createPersonWithRequiredAddress);

    public async Task<Result> UpdatePersonUseCase(UpdatePersonDto updatePerson) =>
        await _updatePersonUseCase.ExecuteAsync(updatePerson);

    public async Task<Result> UpdatePersonWithRequiredAddressUseCase(UpdatePersonWithRequiredAddressDto updatePersonWithRequiredAddress) =>
        await _updatePersonWithRequiredAddressUseCase.ExecuteAsync(updatePersonWithRequiredAddress);

    public async Task<Result> DeletePersonUseCase(Guid id) =>
        await _deletePersonUseCase.ExecuteAsync(id);
}
