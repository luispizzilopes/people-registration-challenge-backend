using Microsoft.EntityFrameworkCore;
using PeopleAPI.Application.UseCases.Person.CreatePerson;
using PeopleAPI.Application.UseCases.Person.UpdatePerson;
using PeopleAPI.Application.Validations.Person.CpfPersonValidate;
using PeopleAPI.Domain.Enums;
using PeopleAPI.Domain.UnitOfWork;
using PeopleAPI.Infrastructure.Context;
using PeopleAPI.Infrastructure.UnitOfWork;

namespace PeopleAPI.Tests.Application.Person
{
    public class UpdatePersonUseCaseTest
    {
        private readonly AppDbContext _context;
        private readonly IUnitOfWork _unitOfWork;
        private readonly CpfPersonValidation _cpfPersonValidation;
        private readonly UpdatePersonUseCase _updatePersonUseCase;

        public UpdatePersonUseCaseTest()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase("DatabaseTest")
                .Options;

            _context = new AppDbContext(options);
            _unitOfWork = new UnitOfWork(_context);
            _cpfPersonValidation = new CpfPersonValidation(_unitOfWork);
            _updatePersonUseCase = new UpdatePersonUseCase(_unitOfWork, _cpfPersonValidation);
        }

        [Fact]
        public async Task Should_Update_Person_Successfully()
        {
            var person = new PeopleAPI.Domain.Entities.Person
            {
                Id = Guid.NewGuid(),
                Name = "Carlos Lima",
                Cpf = "27723418019",
                BirthDate = new DateTimeOffset(1995, 4, 20, 0, 0, 0, TimeSpan.Zero),
                Address = "Rua X",
                Gender = Gender.Male,
                Email = "carlos@email.com",
                Naturality = "São Paulo",
                Nacionality = "Brasileiro"
            };

            await _context.Peoples.AddAsync(person);
            await _context.SaveChangesAsync();

            var updatedDto = new UpdatePersonDto
            {
                Id = person.Id,
                Name = "Carlos Alberto Lima",
                Cpf = "27723418019", 
                BirthDate = person.BirthDate,
                Address = "Rua Y",
                Gender = person.Gender,
                Email = "carlos.alberto@email.com",
                Naturality = person.Naturality,
                Nacionality = person.Nacionality
            };

            var result = await _updatePersonUseCase.ExecuteAsync(updatedDto);

            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task Should_Fail_When_Cpf_Belongs_To_Another_Person()
        {
            var existingPerson = new PeopleAPI.Domain.Entities.Person
            {
                Id = Guid.NewGuid(),
                Name = "Maria Souza",
                Cpf = "99999999999",
                BirthDate = new DateTimeOffset(1990, 1, 1, 0, 0, 0, TimeSpan.Zero),
                Address = "Rua A",
                Gender = Gender.Female,
                Email = "maria@email.com",
                Naturality = "RJ",
                Nacionality = "Brasileira"
            };

            var personToUpdate = new PeopleAPI.Domain.Entities.Person
            {
                Id = Guid.NewGuid(),
                Name = "Paulo Mendes",
                Cpf = "88888888888",
                BirthDate = new DateTimeOffset(1988, 6, 15, 0, 0, 0, TimeSpan.Zero),
                Address = "Rua B",
                Gender = Gender.Male,
                Email = "paulo@email.com",
                Naturality = "SP",
                Nacionality = "Brasileiro"
            };

            await _context.Peoples.AddRangeAsync(existingPerson, personToUpdate);
            await _context.SaveChangesAsync();

            var updateDto = new UpdatePersonDto
            {
                Id = personToUpdate.Id,
                Name = "Paulo de Souza Mendes",
                Cpf = "99999999999", 
                BirthDate = personToUpdate.BirthDate,
                Address = "Rua Nova",
                Gender = personToUpdate.Gender,
                Email = "paulo.novo@email.com",
                Naturality = personToUpdate.Naturality,
                Nacionality = personToUpdate.Nacionality
            };

            var result = await _updatePersonUseCase.ExecuteAsync(updateDto);

            Assert.False(result.IsSuccess);
            Assert.Equal("O CPF informado já está em uso por outra pessoa!", result.ErrorMessage);
        }
    }
}
