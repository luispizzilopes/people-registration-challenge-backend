using PeopleAPI.Application.UseCases.Authentication.SignIn;
using PeopleAPI.Domain.UnitOfWork;
using PeopleAPI.Infrastructure.Context;
using PeopleAPI.Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using PeopleAPI.Application.Services.TokenJwt;

namespace PeopleAPI.Tests.Application.Authentication;

public class SignInUseCaseTest
{
    private readonly AppDbContext _context;
    private readonly IUnitOfWork _unitOfWork;
    private readonly SignInUseCase _signInUseCase;

    public SignInUseCaseTest()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase("DatabaseTest")
            .Options;

        _context = new AppDbContext(options);
        _unitOfWork = new UnitOfWork(_context);

        var fakeTokenService = new FakeTokenJwtService();
        _signInUseCase = new SignInUseCase(_unitOfWork, fakeTokenService);
    }

    [Fact]
    public async Task Should_SignIn_Successfully_With_Valid_Credentials()
    {
        var user = new PeopleAPI.Domain.Entities.User
        {
            Id = Guid.NewGuid(),
            Email = "usuario@email.com",
            Password = "123456"
        };

        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();

        var dto = new SignInDto
        {
            Email = "usuario@email.com",
            Password = "123456"
        };

        var result = await _signInUseCase.ExecuteAsync(dto);

        Assert.True(result.IsSuccess);
        Assert.Equal("Autenticação realizada com sucesso!", result.SuccessMessage);
        Assert.NotNull(result.Value);
    }

    [Fact]
    public async Task Should_Fail_SignIn_With_Invalid_Credentials()
    {
        var dto = new SignInDto
        {
            Email = "naoexiste@email.com",
            Password = "errada"
        };

        var result = await _signInUseCase.ExecuteAsync(dto);

        Assert.False(result.IsSuccess);
        Assert.Equal("As credenciais informadas são inválidas!", result.ErrorMessage);
    }

    private class FakeTokenJwtService : ITokenJwtService
    {
        public TokenDto CreateTokenUser(PeopleAPI.Domain.Entities.User user)
        {
            return new TokenDto { Token = "fake", DateExpiration = DateTimeOffset.UtcNow.AddMinutes(1) };
        }
    }
}
