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
        return await _createPersonUseCase
            .ExecuteAsync(createPersonWithRequiredAddress.Adapt<CreatePersonDto>()); 
    }
}
