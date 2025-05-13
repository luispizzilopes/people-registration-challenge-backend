using Mapster;
using PeopleAPI.Application.Validations.Person.CpfPersonValidate;
using PeopleAPI.Domain.UnitOfWork;
using PeopleAPI.Shared.Common;

namespace PeopleAPI.Application.UseCases.Person.CreatePerson;

public class CreatePersonUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly CpfPersonValidation _cpfPersonValidation;

    public CreatePersonUseCase(IUnitOfWork unitOfWork, CpfPersonValidation cpfPersonValidation)
    {
        _unitOfWork = unitOfWork;
        _cpfPersonValidation = cpfPersonValidation;
    }

    public async Task<Result> ExecuteAsync(CreatePersonDto createPerson)
    {
        var validationResult = await ValidateCpfPerson(createPerson.Cpf);
        if (!validationResult.IsSuccess)
            return validationResult;

        var personEntity = createPerson.Adapt<Domain.Entities.Person>();
        bool created = await _unitOfWork.PersonRepository.CreatePerson(personEntity);

        if (!created)
            return Result.Failure("Não foi possível criar a pessoa!");

        await _unitOfWork.Commit();
        return Result.Success("Pessoa criada com sucesso!");
    }

    private async Task<Result> ValidateCpfPerson(string cpf)
    {
        bool existsCpf = await _cpfPersonValidation.ExecuteAsync(cpf);
        return existsCpf
            ? Result.Failure("O CPF informado já existe na base de dados!")
            : Result.Success(string.Empty);
    }
}
