using Mapster;
using PeopleAPI.Domain.UnitOfWork;
using PeopleAPI.Shared.Common;

namespace PeopleAPI.Application.UseCases.Person.GetPerson; 

public class GetPersonUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public GetPersonUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ResultWithValue<PersonDto>> ExecuteAsync(Guid id)
    {
        var person = await _unitOfWork.PersonRepository.GetPerson(id);

        if (person is null)
            return ResultWithValue<PersonDto>.Failure("Pessoa não encontrada");

        return ResultWithValue<PersonDto>
            .Success(person.Adapt<PersonDto>(), string.Empty);
    }
}
