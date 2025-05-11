using Microsoft.EntityFrameworkCore;
using PeopleAPI.Domain.Entities;
using PeopleAPI.Domain.Interfaces;
using PeopleAPI.Infrastructure.Context;

namespace PeopleAPI.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context; 

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetUser(Guid id)
    {
        return await _context.Users
            .AsNoTracking()
            .AsQueryable()
            .Where(u => u.Id == id)
            .FirstOrDefaultAsync(); 
    }

    public async Task<IEnumerable<User>> GetUsers()
    {
        return await _context.Users.ToListAsync(); 
    }

    public async Task<bool> CreateUser(User user)
    {
        try
        {
            await _context.Users.AddAsync(user);
            return true;
        }
        catch
        {
            return false; 
        }
    }

    public bool UpdateUser(User user)
    {
        try
        {
            _context.Entry(user).State = EntityState.Modified;
            return true;
        }
        catch
        {
            return false;
        }
    }

    public bool DeleteUser(User user)
    {
        try
        {
            _context.Users.Remove(user);
            return true;
        }
        catch
        {
            return false;
        }
    }
}
