using PeopleAPI.Domain.Interfaces;
using PeopleAPI.Domain.UnitOfWork;
using PeopleAPI.Infrastructure.Context;
using PeopleAPI.Infrastructure.Repositories;

namespace PeopleAPI.Infrastructure.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    public AppDbContext _context; 
    private PersonRepository _personRepository;
    private UserRepository _userRepository; 

    public UnitOfWork(AppDbContext context)
    {
        _context = context; 
    }

    public IPersonRepository PersonRepository
    {
        get
        {
            return _personRepository = _personRepository ?? new PersonRepository(_context); 
        }

        set { }
    }

    public IUserRepository UserRepository
    {
        get
        {
            return _userRepository = _userRepository ?? new UserRepository(_context); 
        }

        set { }
    }

    public async Task Commit()
    {
        await _context.SaveChangesAsync();
    }
}
