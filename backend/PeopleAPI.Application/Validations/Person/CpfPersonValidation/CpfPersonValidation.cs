using PeopleAPI.Domain.UnitOfWork;

namespace PeopleAPI.Application.Validations.Person.CpfPersonValidate; 

public class CpfPersonValidation
{
    private readonly IUnitOfWork _unitOfWork;

    public CpfPersonValidation(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> ExecuteAsync(string cpf, Guid? id = null)
    {
        return id.HasValue
            ? await _unitOfWork.PersonRepository.ExistsCpf(cpf, id.Value)
            : await _unitOfWork.PersonRepository.ExistsCpf(cpf);
    }
}
