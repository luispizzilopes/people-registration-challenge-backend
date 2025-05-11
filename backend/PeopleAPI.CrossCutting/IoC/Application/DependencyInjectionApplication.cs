using Microsoft.Extensions.DependencyInjection;

namespace PeopleAPI.CrossCutting.IoC.Application; 

public class DependencyInjectionApplication
{
    public static IServiceCollection AddApplication(IServiceCollection services)
    {
        return services; 
    }
}
