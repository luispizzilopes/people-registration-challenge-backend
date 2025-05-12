using Mapster;
using PeopleAPI.Application.UseCases.Person.CreatePerson;
using PeopleAPI.Shared.Common;

namespace PeopleAPI.Application.UseCases.Person.CreatePersonWithRequiredAddress;

public class CreatePersonWithRequiredAddressUseCase
{
    private readonly CreatePersonUseCase _createPersonUseCase;

    public CreatePersonWithRequiredAddressUseCase(CreatePersonUseCase createPersonUseCase)
    {
        _createPersonUseCase = createPersonUseCase;
    }

    public async Task<Result> ExecuteAsync(CreatePersonWithRequiredAddressDto createPersonWithRequiredAddress)
    {
        if (string.IsNullOrEmpty(createPersonWithRequiredAddress.Address))
            return Result.Failure("O campo Endereço é obrigatório."); 

        return await _createPersonUseCase
            .ExecuteAsync(createPersonWithRequiredAddress.Adapt<CreatePersonDto>()); 
    }
}
