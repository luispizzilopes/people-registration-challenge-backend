using Microsoft.Extensions.DependencyInjection;
using PeopleAPI.Domain.Interfaces;
using PeopleAPI.Domain.UnitOfWork;
using PeopleAPI.Infrastructure.Repositories;
using PeopleAPI.Infrastructure.UnitOfWork;
using System.Runtime.CompilerServices;

namespace PeopleAPI.CrossCutting.IoC.Infrastructure; 

public class DependencyInjectionInfrastructure
{
    public static IServiceCollection AddInfrastructure(IServiceCollection services)
    {
        services.AddScoped<IPersonRepository, PersonRepository>(); 
        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services; 
    }
}
