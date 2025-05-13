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
        if (string.IsNullOrEmpty(updatePersonWithRequiredAddress.Address))
            return Result.Failure("O campo Endereço é obrigatório.");

        return await _updatePersonUseCase
            .ExecuteAsync(updatePersonWithRequiredAddress.Adapt<UpdatePersonDto>()); 
    } 
}
