using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using PeopleAPI.Application.Facades.Authentication;
using PeopleAPI.Application.Facades.Person;
using PeopleAPI.Application.Facades.User;
using PeopleAPI.Application.Mappings;
using PeopleAPI.Application.Services.TokenJwt;
using PeopleAPI.Application.UseCases.Authentication.SignIn;
using PeopleAPI.Application.UseCases.Person.CreatePerson;
using PeopleAPI.Application.UseCases.Person.CreatePersonWithRequiredAddress;
using PeopleAPI.Application.UseCases.Person.DeletePerson;
using PeopleAPI.Application.UseCases.Person.GetPerson;
using PeopleAPI.Application.UseCases.Person.GetPersons;
using PeopleAPI.Application.UseCases.Person.UpdatePerson;
using PeopleAPI.Application.UseCases.Person.UpdatePersonWithRequiredAddress;
using PeopleAPI.Application.UseCases.User.CreateUser;
using PeopleAPI.Application.Validations.Person.CpfPersonValidate;
using System.Text;

namespace PeopleAPI.CrossCutting.IoC.Application; 

public static class DependencyInjectionApplication
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<CreatePersonUseCase>(); 
        services.AddScoped<UpdatePersonUseCase>();
        services.AddScoped<GetPersonUseCase>();
        services.AddScoped<GetPersonsUseCase>();
        services.AddScoped<DeletePersonUseCase>();
        services.AddScoped<CreatePersonWithRequiredAddressUseCase>();
        services.AddScoped<UpdatePersonWithRequiredAddressUseCase>();
        services.AddScoped<SignInUseCase>();
        services.AddScoped<CreateUserUseCase>();

        services.AddScoped<ITokenJwtService, TokenJwtService>(); 
        
        services.AddScoped<CpfPersonValidation>();

        services.AddScoped<IPersonFacade, PersonFacade>();
        services.AddScoped<IUserFacade, UserFacade>();
        services.AddScoped<IAuthenticationFacade, AuthenticationFacade>();

        services.AddAuthentication(
           JwtBearerDefaults.AuthenticationScheme).
           AddJwtBearer(options =>
           options.TokenValidationParameters = new TokenValidationParameters
           {
               ValidateIssuer = true,
               ValidateAudience = true,
               ValidateLifetime = true,
               ValidAudience = configuration["TokenConfiguration:Audience"],
               ValidIssuer = configuration["TokenConfiguration:Issuer"],
               ValidateIssuerSigningKey = true,
               IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:key"]!))
           });

        MapsterConfiguration.RegisterMappings();

        return services; 
    }
}
