using PeopleAPI.Domain.Exception.Messages;
using PeopleAPI.Domain.Exception;
using Microsoft.EntityFrameworkCore;
using PeopleAPI.Domain.UnitOfWork;
using PeopleAPI.Infrastructure.Context;
using PeopleAPI.Infrastructure.UnitOfWork;

namespace PeopleAPI.Tests.Infrastructure.User; 

public class UserRepositoryTest
{
    private readonly AppDbContext _context;
    private readonly IUnitOfWork _unitOfWork;

    public UserRepositoryTest()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
          .UseInMemoryDatabase("DatabaseTest")
          .Options;

        _context = new AppDbContext(options);
        _unitOfWork = new UnitOfWork(_context);
    }

    [Fact]
    public async Task Should_Create_New_User_When_Email_And_Password_Are_Valid()
    {
        var user = PeopleAPI.Domain.Entities.User.Create("valid@email.com", "StrongPassword1");

        var result = await _unitOfWork.UserRepository.CreateUser(user);
        await _context.SaveChangesAsync();

        var saved = await _unitOfWork.UserRepository.GetUser(user.Id);

        Assert.True(result);
        Assert.NotNull(saved);
        Assert.Equal("valid@email.com", saved?.Email);
    }

    [Fact]
    public void Should_Throw_Exception_When_Email_Is_Invalid()
    {
        var exception = Assert.Throws<DomainException>(() =>
            PeopleAPI.Domain.Entities.User.Create("invalid-email", "StrongPassword1"));

        Assert.Equal(UserMessagesException.EmailInvalid, exception.Message);
    }

    [Fact]
    public void Should_Throw_Exception_When_Password_Is_Invalid()
    {
        var exception = Assert.Throws<DomainException>(() =>
            PeopleAPI.Domain.Entities.User.Create("user@example.com", "weak"));

        Assert.Equal(UserMessagesException.PasswordInvalid, exception.Message);
    }

    [Fact]
    public async Task Should_Delete_Existing_User()
    {
        var user = PeopleAPI.Domain.Entities.User.Create("delete@email.com", "Delete123");

        await _unitOfWork.UserRepository.CreateUser(user);
        await _context.SaveChangesAsync();

        var result = _unitOfWork.UserRepository.DeleteUser(user);
        await _context.SaveChangesAsync();

        var deleted = await _unitOfWork.UserRepository.GetUser(user.Id);

        Assert.True(result);
        Assert.Null(deleted);
    }

    [Fact]
    public async Task Should_Update_User_Successfully()
    {
        var user = PeopleAPI.Domain.Entities.User.Create("update@email.com", "Update123");
        await _unitOfWork.UserRepository.CreateUser(user);
        await _context.SaveChangesAsync();

        user.Email = "updated@email.com"; 

        var result = _unitOfWork.UserRepository.UpdateUser(user);
        await _context.SaveChangesAsync();

        var saved = await _unitOfWork.UserRepository.GetUser(user.Id);

        Assert.True(result);
        Assert.Equal("updated@email.com", saved?.Email);
    }

    [Fact]
    public async Task Should_Get_All_Users()
    {
        var user1 = PeopleAPI.Domain.Entities.User.Create("user1@email.com", "Password1A");
        var user2 = PeopleAPI.Domain.Entities.User.Create("user2@email.com", "Password2B");

        await _unitOfWork.UserRepository.CreateUser(user1);
        await _unitOfWork.UserRepository.CreateUser(user2);
        await _context.SaveChangesAsync();

        var result = await _unitOfWork.UserRepository.GetUsers();

        Assert.Contains(result, u => u.Email == user1.Email);
        Assert.Contains(result, u => u.Email == user2.Email);
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task Should_Throw_When_Context_Disposed_Before_Operation()
    {
        var user = PeopleAPI.Domain.Entities.User.Create("dispose@test.com", "Password123");

        _context.Dispose();

        await Assert.ThrowsAsync<ObjectDisposedException>(async () =>
        {
            await _unitOfWork.UserRepository.CreateUser(user);
            await _context.SaveChangesAsync();
        });
    }
}
