using Microsoft.EntityFrameworkCore;
using PeopleAPI.Domain.UnitOfWork;
using PeopleAPI.Infrastructure.Context;
using PeopleAPI.Infrastructure.UnitOfWork;
using PeopleAPI.Tests.Data;

namespace PeopleAPI.Tests.Infrastructure.Person; 

public class PersonRepositoryTest
{
    private readonly AppDbContext _context;
    private readonly IUnitOfWork _unitOfWork;

    public PersonRepositoryTest()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
          .UseInMemoryDatabase("DatabaseTest")
          .Options;

        _context = new AppDbContext(options);
        _unitOfWork = new UnitOfWork(_context);
    }

    [Fact]
    public async Task Should_Insert_New_Person_Into_Database()
    {
        var entity = new PersonBuilder()
            .WithName("Bruno Carvalho")
            .WithCpf("55731980098")
            .Build();

        await _unitOfWork.PersonRepository.CreatePerson(entity);
        await _context.SaveChangesAsync();

        var result = await _unitOfWork.PersonRepository.GetPerson(entity.Id);

        Assert.NotNull(result);
        Assert.Equal(entity.Name, result?.Name);
    }

    [Fact]
    public async Task Should_Throw_When_Context_Is_Disposed_Before_Save()
    {
        var entity = PersonTestData.GetValidPerson();
        _context.Dispose(); 

        await Assert.ThrowsAsync<ObjectDisposedException>(async () =>
        {
            await _unitOfWork.PersonRepository.CreatePerson(entity);
            await _context.SaveChangesAsync();
        });
    }

    [Fact]
    public async Task Should_Delete_Existing_Person_From_Database()
    {
        var entity = new PersonBuilder()
            .WithName("Juliana Alves")
            .WithCpf("55731980098")
            .Build();

        await _unitOfWork.PersonRepository.CreatePerson(entity);
        await _context.SaveChangesAsync();

        _unitOfWork.PersonRepository.DeletePerson(entity);
        await _context.SaveChangesAsync();

        var result = await _unitOfWork.PersonRepository.GetPerson(entity.Id);

        Assert.Null(result);
    }

    [Fact]
    public async Task Should_Find_Person_By_Id_If_Exists()
    {
        var entity = new PersonBuilder()
            .WithName("Fernanda Costa")
            .WithCpf("55731980098")
            .Build();

        await _unitOfWork.PersonRepository.CreatePerson(entity);
        await _context.SaveChangesAsync();

        var result = await _unitOfWork.PersonRepository.GetPerson(entity.Id);

        Assert.NotNull(result);
        Assert.Equal(entity.Id, result?.Id);
    }

    [Fact]
    public async Task Should_Get_All_People_From_Database()
    {
        var entity1 = new PersonBuilder().WithName("Vinícius Rocha").WithCpf("55731980098").Build();
        var entity2 = new PersonBuilder().WithName("Elaine Silva").Build();

        await _unitOfWork.PersonRepository.CreatePerson(entity1);
        await _unitOfWork.PersonRepository.CreatePerson(entity2);
        await _context.SaveChangesAsync();

        var result = await _unitOfWork.PersonRepository.GetPersons();

        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task Should_Return_True_If_Cpf_Already_Exists()
    {
        var entity = new PersonBuilder().WithCpf("55731980098").Build();
        await _unitOfWork.PersonRepository.CreatePerson(entity);
        await _context.SaveChangesAsync();

        var exists = await _unitOfWork.PersonRepository.ExistsCpf("55731980098");

        Assert.True(exists);
    }

    [Fact]
    public async Task Should_Return_False_If_Cpf_Does_Not_Exist()
    {
        var exists = await _unitOfWork.PersonRepository.ExistsCpf("55731980098");

        Assert.False(exists);
    }
}
