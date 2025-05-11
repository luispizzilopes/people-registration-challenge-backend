using Mapster;
using PeopleAPI.Domain.UnitOfWork;
using PeopleAPI.Shared.Common;

namespace PeopleAPI.Application.UseCases.Person.CreatePerson; 

public class CreatePersonUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public CreatePersonUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork; 
    }

    public async Task<Result> ExecuteAsync(CreatePersonDto createPerson)
    {
        bool response = await _unitOfWork.PersonRepository
            .CreatePerson(createPerson.Adapt<Domain.Entities.Person>());

        if (response)
        {
            await _unitOfWork.Commit();
            return Result.Success("Pessoa criada com sucesso!"); 
        }

        return Result.Failure("Não foi possível criar a pessoa!"); 
    }
}
