using Microsoft.Extensions.Configuration;
using PeopleAPI.Application.Services.TokenJwt;

namespace PeopleAPI.Tests.Application.Services;

public class TokenJwtServiceTests
{
    private readonly IConfiguration _configuration;

    public TokenJwtServiceTests()
    {
        var inMemorySettings = new Dictionary<string, string> {
            {"Jwt:key", "MinhaChaveSuperSecretaJwtComMaisDe32Chars"},
            {"TokenConfiguration:Issuer", "PeopleAPI"},
            {"TokenConfiguration:Audience", "PeopleAPIUser"},
            {"TokenConfiguration:ExpireHours", "1"}
        };

        _configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();
    }

    [Fact]
    public void CreateTokenUser_WhenCalled_ShouldReturnValidToken()
    {
        var service = new TokenJwtService(_configuration);
        var user = new PeopleAPI.Domain.Entities.User { Email = "usuario@teste.com" };

        var tokenDto = service.CreateTokenUser(user);

        Assert.False(string.IsNullOrWhiteSpace(tokenDto.Token));
        Assert.True(tokenDto.DateExpiration > DateTime.Now);
    }

    [Fact]
    public void CreateTokenUser_WhenCalled_ShouldContainEmailInToken()
    {
        var service = new TokenJwtService(_configuration);
        var user = new PeopleAPI.Domain.Entities.User { Email = "usuario@teste.com" };

        var tokenDto = service.CreateTokenUser(user);

        var handler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
        var jwt = handler.ReadJwtToken(tokenDto.Token);

        Assert.Contains(jwt.Claims, c => c.Type == System.Security.Claims.ClaimTypes.Email && c.Value == user.Email);
    }

    [Fact]
    public void CreateTokenUser_WhenCalled_ShouldGenerateCorrectExpirationDate()
    {
        var service = new TokenJwtService(_configuration);
        var user = new PeopleAPI.Domain.Entities.User { Email = "usuario@teste.com" };

        var tokenDto = service.CreateTokenUser(user);
        var expected = DateTime.Now.AddHours(1);

        Assert.True(tokenDto.DateExpiration > DateTime.Now);
        Assert.True((tokenDto.DateExpiration - expected).TotalSeconds < 10);
    }

}
