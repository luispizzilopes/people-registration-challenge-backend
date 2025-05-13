using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PeopleAPI.Domain.Interfaces;
using PeopleAPI.Domain.UnitOfWork;
using PeopleAPI.Infrastructure.Context;
using PeopleAPI.Infrastructure.Repositories;
using PeopleAPI.Infrastructure.UnitOfWork;
using System.Runtime.CompilerServices;

namespace PeopleAPI.CrossCutting.IoC.Infrastructure; 

public static class DependencyInjectionInfrastructure
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>(opt =>
          opt.UseInMemoryDatabase("Database"));

        services.AddScoped<IPersonRepository, PersonRepository>(); 
        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services; 
    }
}
