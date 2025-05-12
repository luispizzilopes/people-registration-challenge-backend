using Microsoft.EntityFrameworkCore;
using PeopleAPI.Application.UseCases.Person.CreatePerson;
using PeopleAPI.Application.UseCases.Person.DeletePerson;
using PeopleAPI.Application.Validations.Person.CpfPersonValidate;
using PeopleAPI.Domain.Enums;
using PeopleAPI.Domain.UnitOfWork;
using PeopleAPI.Infrastructure.Context;
using PeopleAPI.Infrastructure.UnitOfWork;

namespace PeopleAPI.Tests.Application.Person;

public class DeletePersonUseCaseTest
{
    private readonly AppDbContext _context;
    private readonly IUnitOfWork _unitOfWork;
    private readonly DeletePersonUseCase _deletePersonUseCase;
    private readonly CreatePersonUseCase _createPersonUseCase;
    private readonly CpfPersonValidation _cpfPersonValidation;

    public DeletePersonUseCaseTest()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase("DatabaseTest")
            .Options;

        _context = new AppDbContext(options);
        _unitOfWork = new UnitOfWork(_context);
        _cpfPersonValidation = new CpfPersonValidation(_unitOfWork);
        _deletePersonUseCase = new DeletePersonUseCase(_unitOfWork);
        _createPersonUseCase = new CreatePersonUseCase(_unitOfWork, _cpfPersonValidation);
    }


    [Fact]
    public async Task Should_Delete_Person_Successfully()
    {
        var person = new CreatePersonDto
        {
            Name = "Maria Oliveira",
            Cpf = "78393002001",
            BirthDate = new DateTimeOffset(1985, 3, 25, 0, 0, 0, TimeSpan.Zero),
            Address = "Rua B",
            Gender = Gender.Female,
            Email = "maria@email.com",
            Naturality = "São Paulo",
            Nacionality = "Brasileira"
        };

        await _createPersonUseCase.ExecuteAsync(person);

        var entity = await _context.Peoples.FirstOrDefaultAsync();

        var result = await _deletePersonUseCase.ExecuteAsync(entity!.Id);

        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async Task Should_Return_Failure_When_Person_Not_Found()
    {
        var nonExistentId = Guid.NewGuid();

        var result = await _deletePersonUseCase.ExecuteAsync(nonExistentId);

        Assert.False(result.IsSuccess);
    }
}
