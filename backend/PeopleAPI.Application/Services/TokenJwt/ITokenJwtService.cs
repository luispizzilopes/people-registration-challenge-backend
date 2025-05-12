using PeopleAPI.Domain.Entities;

namespace PeopleAPI.Application.Services.TokenJwt; 

public interface ITokenJwtService
{
    TokenDto CreateTokenUser(User user); 
}
