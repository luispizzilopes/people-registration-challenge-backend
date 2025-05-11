using Mapster;
using PeopleAPI.Application.UseCases.Person.CreatePerson;
using PeopleAPI.Application.UseCases.Person.UpdatePerson;
using PeopleAPI.Shared.Common;

namespace PeopleAPI.Application.UseCases.Person.UpdatePersonWithRequiredAddress; 

public class UpdatePersonWithRequiredAddressUseCase
{
    private readonly UpdatePersonUseCase _updatePersonUseCase;

    public UpdatePersonWithRequiredAddressUseCase(UpdatePersonUseCase updatePersonUseCase)
    {
        _updatePersonUseCase = updatePersonUseCase;
    }

    public async Task<Result> ExecuteAsync(UpdatePersonWithRequiredAddressDto updatePersonWithRequiredAddress)
    {
        return await _updatePersonUseCase
            .ExecuteAsync(updatePersonWithRequiredAddress.Adapt<UpdatePersonDto>()); 
    } 
}
