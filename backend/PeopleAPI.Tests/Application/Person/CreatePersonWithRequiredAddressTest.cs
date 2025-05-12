using System;
using System.Threading.Tasks;
using Xunit;
using PeopleAPI.Application.UseCases.Person.CreatePerson;
using PeopleAPI.Application.UseCases.Person.CreatePersonWithRequiredAddress;
using PeopleAPI.Infrastructure.Repositories;
using PeopleAPI.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using PeopleAPI.Infrastructure.UnitOfWork;
using PeopleAPI.Infrastructure.Context;
using PeopleAPI.Application.Validations.Person.CpfPersonValidate;
using PeopleAPI.Domain.UnitOfWork;

namespace PeopleAPI.Tests.Application.UseCases.Person; 

public class CreatePersonWithRequiredAddressUseCaseTest
{
    private readonly AppDbContext _context;
    private readonly IUnitOfWork _unitOfWork;
    private readonly CreatePersonUseCase _createPersonUseCase;
    private readonly CreatePersonWithRequiredAddressUseCase _createPersonWithRequiredAddressUseCase; 
    private readonly CpfPersonValidation _cpfPersonValidation;

    public CreatePersonWithRequiredAddressUseCaseTest()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase("DatabaseTest")
            .Options;

        _context = new AppDbContext(options);
        _unitOfWork = new UnitOfWork(_context);
        _cpfPersonValidation = new CpfPersonValidation(_unitOfWork);
        _createPersonUseCase = new CreatePersonUseCase(_unitOfWork, _cpfPersonValidation);
        _createPersonWithRequiredAddressUseCase = new CreatePersonWithRequiredAddressUseCase(_createPersonUseCase);
    }

    [Fact]
    public async Task Should_Return_Failure_When_Address_Is_Missing()
    {
        var dto = new CreatePersonWithRequiredAddressDto
        {
            Cpf = "12345678901",
            Name = "João da Silva",
            BirthDate = new DateTimeOffset(1990, 5, 10, 0, 0, 0, TimeSpan.Zero),
            Address = null, 
            Gender = Gender.Male,
            Email = "joao@email.com",
            Naturality = "Brasília",
            Nacionality = "Brasileiro"
        };

        var result = await _createPersonWithRequiredAddressUseCase.ExecuteAsync(dto);

        Assert.False(result.IsSuccess);
    }
}
