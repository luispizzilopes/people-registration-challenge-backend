using PeopleAPI.Application.UseCases.User.CreateUser;
using PeopleAPI.Domain.UnitOfWork;
using PeopleAPI.Infrastructure.Context;
using PeopleAPI.Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace PeopleAPI.Tests.Application.User;

public class CreateUserUseCaseTest
{
    private readonly AppDbContext _context;
    private readonly IUnitOfWork _unitOfWork;
    private readonly CreateUserUseCase _createUserUseCase;

    public CreateUserUseCaseTest()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase("DatabaseTest")
            .Options;

        _context = new AppDbContext(options);
        _unitOfWork = new UnitOfWork(_context);
        _createUserUseCase = new CreateUserUseCase(_unitOfWork);
    }

    [Fact]
    public async Task Should_Create_User_When_Email_Is_Unique()
    {
        var dto = new CreateUserDto
        {
            Email = "novo@email.com",
            Password = "Senha123"
        };

        var result = await _createUserUseCase.ExecuteAsync(dto);

        Assert.True(result.IsSuccess);
        Assert.Equal("Usuário criado com sucesso!", result.SuccessMessage);

        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
        Assert.NotNull(user);
        Assert.Equal(dto.Email, user.Email);
    }

    [Fact]
    public async Task Should_Fail_When_Email_Already_Exists()
    {
        var existingUser = new PeopleAPI.Domain.Entities.User
        {
            Id = Guid.NewGuid(),
            Email = "existente@email.com",
            Password = "senhaantiga"
        };

        await _context.Users.AddAsync(existingUser);
        await _context.SaveChangesAsync();

        var dto = new CreateUserDto
        {
            Email = "existente@email.com",
            Password = "novaSenha"
        };

        var result = await _createUserUseCase.ExecuteAsync(dto);

        Assert.False(result.IsSuccess);
        Assert.Equal("O Email informado já existe na base de dados!", result.ErrorMessage);
    }
}
