using Microsoft.EntityFrameworkCore;
using PeopleAPI.Domain.Entities;
using PeopleAPI.Domain.Interfaces;
using PeopleAPI.Infrastructure.Context;

namespace PeopleAPI.Infrastructure.Repositories;

public class PersonRepository : IPersonRepository
{
    private readonly AppDbContext _context; 

    public PersonRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Person?> GetPerson(Guid id)
    {
        return await _context.Peoples
            .AsNoTracking()
            .AsQueryable()
            .Where(p => p.Id == id)
            .FirstOrDefaultAsync(); 
    }

    public async Task<IEnumerable<Person>> GetPersons()
    {
        return await _context.Peoples
            .ToListAsync(); 
    }

    public async Task<bool> CreatePerson(Person person)
    {
        try
        {
            await _context.AddAsync(person);
            return true; 
        }
        catch
        {
            return false; 
        }
    }
    public bool UpdatePerson(Person person)
    {
        try
        {
            _context.Entry(person).State = EntityState.Modified;
            return true; 
        }
        catch
        {
            return false; 
        }
    }

    public bool DeletePerson(Person person)
    {
        try
        {
            _context.Remove(person); 
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> ExistsCpf(string cpf)
    {
        return await _context.Peoples
            .AsNoTracking()
            .Where(p => p.Cpf == cpf)
            .AnyAsync(); 
    }

    public async Task<bool> ExistsCpf(string cpf, Guid id)
    {
        return await _context.Peoples
         .AsNoTracking()
         .Where(p => p.Cpf == cpf && p.Id != id)
         .AnyAsync();
    }
}
