using Mapster;
using PeopleAPI.Application.UseCases.Person.CreatePerson;
using PeopleAPI.Application.UseCases.Person.CreatePersonWithRequiredAddress;
using PeopleAPI.Application.UseCases.Person.UpdatePersonWithRequiredAddress;
using PeopleAPI.Domain.Entities;

namespace PeopleAPI.Application.Mappings; 

public class MapsterConfiguration
{
    public static void RegisterMappings()
    {
        TypeAdapterConfig<CreatePersonDto, Person>.NewConfig()
            .MapWith(src => Person.Create(
                src.Name,
                src.BirthDate,
                src.Cpf,
                src.Address,
                src.Gender,
                src.Email,
                src.Naturality,
                src.Nacionality
            ));

        TypeAdapterConfig<UpdatePersonDto, Person>.NewConfig()
            .MapWith(src => Person.Create(
                src.Name,
                src.BirthDate,
                src.Cpf,
                src.Address,
                src.Gender,
                src.Email,
                src.Naturality,
                src.Nacionality
            ));

        TypeAdapterConfig<CreatePersonWithRequiredAddressDto, Person>.NewConfig()
            .MapWith(src => Person.Create(
                src.Name,
                src.BirthDate,
                src.Cpf,
                src.Address,
                src.Gender,
                src.Email,
                src.Naturality,
                src.Nacionality
            ));

        TypeAdapterConfig<UpdatePersonWithRequiredAddressDto, Person>.NewConfig()
            .MapWith(src => Person.Create(
                src.Name,
                src.BirthDate,
                src.Cpf,
                src.Address,
                src.Gender,
                src.Email,
                src.Naturality,
                src.Nacionality
            ));
    }
}
