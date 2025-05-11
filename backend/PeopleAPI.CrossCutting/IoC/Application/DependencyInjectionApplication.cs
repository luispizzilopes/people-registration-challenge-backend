using Microsoft.Extensions.DependencyInjection;
using PeopleAPI.Application.UseCases.Person.CreatePerson;
using PeopleAPI.Application.UseCases.Person.CreatePersonWithRequiredAddress;
using PeopleAPI.Application.UseCases.Person.DeletePerson;
using PeopleAPI.Application.UseCases.Person.GetPerson;
using PeopleAPI.Application.UseCases.Person.GetPersons;
using PeopleAPI.Application.UseCases.Person.UpdatePerson;
using PeopleAPI.Application.UseCases.Person.UpdatePersonWithRequiredAddress;
using PeopleAPI.Application.Validations.Person.CpfPersonValidate;

namespace PeopleAPI.CrossCutting.IoC.Application; 

public class DependencyInjectionApplication
{
    public static IServiceCollection AddApplication(IServiceCollection services)
    {
        services.AddScoped<CreatePersonUseCase>(); 
        services.AddScoped<UpdatePersonUseCase>();
        services.AddScoped<GetPersonUseCase>();
        services.AddScoped<GetPersonsUseCase>();
        services.AddScoped<DeletePersonUseCase>();
        services.AddScoped<CreatePersonWithRequiredAddressUseCase>();
        services.AddScoped<UpdatePersonWithRequiredAddressUseCase>();
        
        services.AddScoped<CpfPersonValidation>(); 

        return services; 
    }
}
