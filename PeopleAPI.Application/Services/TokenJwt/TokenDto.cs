namespace PeopleAPI.Application.Services.TokenJwt; 

public class TokenDto
{
    public string Token { get; set; } = string.Empty;
    public DateTimeOffset DateExpiration { get; set; }
}
