using Mapster;
using PeopleAPI.Application.UseCases.Person.CreatePerson;
using PeopleAPI.Application.Validations.Person.CpfPersonValidate;
using PeopleAPI.Domain.UnitOfWork;
using PeopleAPI.Shared.Common;

namespace PeopleAPI.Application.UseCases.Person.UpdatePerson; 

public class UpdatePersonUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly CpfPersonValidation _cpfPersonValidation;

    public UpdatePersonUseCase(IUnitOfWork unitOfWork, CpfPersonValidation cpfPersonValidation)
    {
        _unitOfWork = unitOfWork;
        _cpfPersonValidation = cpfPersonValidation;
    }

    public async Task<Result> ExecuteAsync(UpdatePersonDto updatePerson)
    {
        var cpfValidationResult = await ValidateCpfPerson(updatePerson.Cpf, updatePerson.Id);
        if (!cpfValidationResult.IsSuccess)
            return cpfValidationResult;

        var personEntity = updatePerson.Adapt<Domain.Entities.Person>();
        personEntity.Id = updatePerson.Id; 
        bool updated = _unitOfWork.PersonRepository.UpdatePerson(personEntity);

        if (!updated)
            return Result.Failure("Não foi possível atualizar a pessoa!");

        await _unitOfWork.Commit();
        return Result.Success("Pessoa atualizada com sucesso!");
    }

    private async Task<Result> ValidateCpfPerson(string cpf, Guid personId)
    {
        bool cpfExistsForAnother = await _cpfPersonValidation.ExecuteAsync(cpf, personId);

        return cpfExistsForAnother
            ? Result.Failure("O CPF informado já está em uso por outra pessoa!")
            : Result.Success(string.Empty);
    }

}
