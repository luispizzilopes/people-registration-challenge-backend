using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using PeopleAPI.Application.Facades.Person;
using PeopleAPI.Application.UseCases.Person.CreatePerson;
using PeopleAPI.Application.UseCases.Person.CreatePersonWithRequiredAddress;
using PeopleAPI.Application.UseCases.Person.DeletePerson;
using PeopleAPI.Application.UseCases.Person.GetPerson;
using PeopleAPI.Application.UseCases.Person.GetPersons;
using PeopleAPI.Application.UseCases.Person.UpdatePerson;
using PeopleAPI.Application.UseCases.Person.UpdatePersonWithRequiredAddress;
using PeopleAPI.Application.Validations.Person.CpfPersonValidate;
using PeopleAPI.Domain.Enums;
using PeopleAPI.Domain.UnitOfWork;
using PeopleAPI.Infrastructure.Context;
using PeopleAPI.Infrastructure.UnitOfWork;
using PeopleAPI.Shared.Common;
using Xunit.Abstractions;

namespace PeopleAPI.Tests.Application.Facades; 

public class PersonFacadeTest
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly PersonFacade _personFacade;
    private readonly CreatePersonUseCase _createPersonUseCase;
    private readonly CreatePersonWithRequiredAddressUseCase _createPersonWithRequiredAddressUseCase;
    private readonly UpdatePersonUseCase _updatePersonUseCase;
    private readonly UpdatePersonWithRequiredAddressUseCase _updatePersonWithRequiredAddressUseCase;
    private readonly GetPersonsUseCase _getPersonsUseCase;
    private readonly GetPersonUseCase _getPersonUseCase;
    private readonly DeletePersonUseCase _deletePersonUseCase;
    private readonly IUnitOfWork _unitOfWork;
    private readonly AppDbContext _appDbContext;
    private readonly CpfPersonValidation _cpfPersonValidation;

    public PersonFacadeTest(ITestOutputHelper output)
    {
        _testOutputHelper = output; 

        var options = new DbContextOptionsBuilder<AppDbContext>()
             .UseInMemoryDatabase("DatabaseTest")
             .Options;

        _appDbContext = new AppDbContext(options); 

        _unitOfWork = new UnitOfWork(_appDbContext);

        _cpfPersonValidation = new CpfPersonValidation(_unitOfWork);

        _createPersonUseCase = new CreatePersonUseCase(_unitOfWork, _cpfPersonValidation);
        _createPersonWithRequiredAddressUseCase = new CreatePersonWithRequiredAddressUseCase(_createPersonUseCase);
        _updatePersonUseCase = new UpdatePersonUseCase(_unitOfWork, _cpfPersonValidation);
        _updatePersonWithRequiredAddressUseCase = new UpdatePersonWithRequiredAddressUseCase(_updatePersonUseCase);
        _getPersonsUseCase = new GetPersonsUseCase(_unitOfWork);
        _getPersonUseCase = new GetPersonUseCase(_unitOfWork);
        _deletePersonUseCase = new DeletePersonUseCase(_unitOfWork);

        _personFacade = new PersonFacade(
            _createPersonUseCase,
            _createPersonWithRequiredAddressUseCase,
            _updatePersonUseCase,
            _updatePersonWithRequiredAddressUseCase,
            _getPersonsUseCase,
            _getPersonUseCase,
            _deletePersonUseCase
        );
    }

    [Fact]
    public async Task CreatePersonUseCase_DeveCriarPessoa()
    {
        var dto = new CreatePersonDto
        {
            Name = "João",
            BirthDate = DateTime.Now,
            Cpf = "11144477735",
            Address = "Rua Exemplo, 123",
            Gender = Gender.Male,
            Email = "joao@email.com",
            Naturality = "SP",
            Nacionality = "Brasileiro"
        };

        var result = await _personFacade.CreatePersonUseCase(dto);

        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async Task GetPersonsUseCase_DeveRetornarListaDePessoas_AposCriarPessoa()
    {
        var dto = new CreatePersonDto
        {
            Name = "João",
            BirthDate = DateTime.Now,
            Cpf = "11144477735",
            Address = "Rua Exemplo, 123",
            Gender = Gender.Male,
            Email = "joao@email.com",
            Naturality = "SP",
            Nacionality = "Brasileiro"
        };

        var resultCreate = await _personFacade.CreatePersonUseCase(dto);

        var resultGet = await _personFacade.GetPersonsUseCase();

        Assert.NotNull(resultGet);
        Assert.Equal(1, resultGet.Count());
    }

    [Fact]
    public async Task GetPersonUseCase_DeveRetornarPessoaPorId_AposCriarPessoa()
    {
        var createResult = await _personFacade.CreatePersonUseCase(new CreatePersonDto
        {
            Name = "Carlos",
            BirthDate = DateTime.Now,
            Cpf = "12345678909",
            Address = "Rua Nova, 789",
            Gender = Gender.Male,
            Email = "carlos@email.com",
            Naturality = "MG",
            Nacionality = "Brasileiro"
        });

        var result = await _personFacade.GetPersonsUseCase();

        Assert.NotNull(result);
        Assert.Equal("Carlos", result.FirstOrDefault()!.Name);
    }
}
