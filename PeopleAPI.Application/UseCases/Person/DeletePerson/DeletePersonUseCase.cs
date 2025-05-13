using PeopleAPI.Domain.UnitOfWork;
using PeopleAPI.Shared.Common;

namespace PeopleAPI.Application.UseCases.Person.DeletePerson; 

public class DeletePersonUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public DeletePersonUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> ExecuteAsync(Guid id)
    {
        var person = await _unitOfWork.PersonRepository.GetPerson(id);
        if (person is null)
            return Result.Failure("Pessoa não encontrada!");

        bool deleted = _unitOfWork.PersonRepository.DeletePerson(person);
        if (!deleted)
            return Result.Failure("Não foi possível deletar a pessoa!");

        await _unitOfWork.Commit();
        return Result.Success("Pessoa deletada com sucesso!");
    }
}
