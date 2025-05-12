using PeopleAPI.Application.UseCases.Person.CreatePerson;
using PeopleAPI.Application.UseCases.Person.CreatePersonWithRequiredAddress;
using PeopleAPI.Application.UseCases.Person.UpdatePersonWithRequiredAddress;
using PeopleAPI.Shared.Common;

namespace PeopleAPI.Application.Facades.Person; 

public interface IPersonFacade
{
    Task<IEnumerable<UseCases.Person.GetPersons.PersonDto>> GetPersonsUseCase();
    Task<ResultWithValue<UseCases.Person.GetPerson.PersonDto>> GetPersonUseCase(Guid id);
    Task<Result> CreatePersonUseCase(CreatePersonDto createPerson); 
    Task<Result> CreatePersonWithRequiredAddressUseCase(CreatePersonWithRequiredAddressDto createPersonWithRequiredAddress);
    Task<Result> UpdatePersonUseCase(UpdatePersonDto updatePerson);
    Task<Result> UpdatePersonWithRequiredAddressUseCase(UpdatePersonWithRequiredAddressDto updatePersonWithRequiredAddress);
    Task<Result> DeletePersonUseCase(Guid id);
}
