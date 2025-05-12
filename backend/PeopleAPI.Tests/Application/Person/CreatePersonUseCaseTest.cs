using PeopleAPI.Application.UseCases.Person.CreatePerson;
using PeopleAPI.Domain.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using PeopleAPI.Domain.Enums;
using PeopleAPI.Infrastructure.Context;
using PeopleAPI.Infrastructure.Repositories;
using PeopleAPI.Infrastructure.UnitOfWork;
using PeopleAPI.Application.Validations.Person.CpfPersonValidate;

namespace PeopleAPI.Tests.Application.Person; 

public class CreatePersonUseCaseTest
{
    private readonly AppDbContext _context;
    private readonly IUnitOfWork _unitOfWork;
    private readonly CreatePersonUseCase _createPersonUseCase;
    private readonly CpfPersonValidation _cpfPersonValidation;

    public CreatePersonUseCaseTest()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase("DatabaseTest")
            .Options;

        _context = new AppDbContext(options);
        _unitOfWork = new UnitOfWork(_context);
        _cpfPersonValidation = new CpfPersonValidation(_unitOfWork);

        _createPersonUseCase = new CreatePersonUseCase(_unitOfWork, _cpfPersonValidation);
    }

    [Fact]
    public async Task Should_Insert_New_Person_Into_Database()
    {
        var personDto = new CreatePersonDto
        {
            Cpf = "40620580062",
            Name = "João da Silva",
            BirthDate = new DateTimeOffset(1990, 5, 10, 0, 0, 0, TimeSpan.Zero),
            Address = "Rua A",
            Gender = Gender.Male,
            Email = "joao@email.com",
            Naturality = "Brasília",
            Nacionality = "Brasileiro"
        };

        var result = await _createPersonUseCase.ExecuteAsync(personDto);

        Assert.True(result.IsSuccess);
        Assert.Equal("Pessoa criada com sucesso!", result.SuccessMessage);

        var person = await _context.Peoples.FirstOrDefaultAsync(p => p.Cpf == personDto.Cpf);
        Assert.NotNull(person);
        Assert.Equal(personDto.Name, person.Name);
        Assert.Equal(personDto.Cpf, person.Cpf);
    }

    [Fact]
    public async Task Should_Return_Failure_When_Cpf_Already_Exists()
    {
        var existingPerson = new PeopleAPI.Domain.Entities.Person
        {
            Cpf = "48787696886",
            Name = "João da Silva",
            BirthDate = new DateTimeOffset(1990, 5, 10, 0, 0, 0, TimeSpan.Zero),
            Address = "Rua A",
            Gender = Gender.Male,
            Email = "joao@email.com",
            Naturality = "Brasília",
            Nacionality = "Brasileiro"
        };

        await _context.Peoples.AddAsync(existingPerson);
        await _context.SaveChangesAsync();

        var personDto = new CreatePersonDto
        {
            Cpf = "48787696886",
            Name = "João da Silva",
            BirthDate = new DateTimeOffset(1990, 5, 10, 0, 0, 0, TimeSpan.Zero),
            Address = "Rua B",
            Gender = Gender.Male,
            Email = "joao2@email.com",
            Naturality = "Brasília",
            Nacionality = "Brasileiro"
        };

        var result = await _createPersonUseCase.ExecuteAsync(personDto);

        Assert.False(result.IsSuccess);
    }
}
