using Mapster;
using PeopleAPI.Application.UseCases.Person.GetPerson;
using PeopleAPI.Domain.UnitOfWork;

namespace PeopleAPI.Application.UseCases.Person.GetPersons;

public class GetPersonsUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public GetPersonsUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<PersonDto>> ExecuteAsync()
    {
        var persons = await _unitOfWork.PersonRepository.GetPersons();
        return persons.Adapt<IEnumerable<PersonDto>>(); 
    }
}
