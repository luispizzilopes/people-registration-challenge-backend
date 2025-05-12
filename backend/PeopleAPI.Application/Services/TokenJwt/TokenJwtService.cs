using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PeopleAPI.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PeopleAPI.Application.Services.TokenJwt;

public class TokenJwtService : ITokenJwtService
{
    private readonly IConfiguration _configuration;

    public TokenJwtService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public TokenDto CreateTokenUser(User user)
    {
        TokenDto token = new();

        AssigningToken(token, user);

        return token;
    }

    private void AssigningToken(TokenDto token, User user)
    {
        JwtSecurityTokenHandler handler = new();

        token.Token = handler.WriteToken(CreateJwtSecurityToken(user));
        token.DateExpiration = CreateExpirationDate();
    }

    private JwtSecurityToken CreateJwtSecurityToken(User user)
    {
        Claim[] claims = CreateUserClaims(user);
        SigningCredentials signingCredentials = CreateSigningCredentials();
        DateTime expirationDate = CreateExpirationDate();

        return new JwtSecurityToken(
              issuer: _configuration["TokenConfiguration:Issuer"],
              audience: _configuration["TokenConfiguration:Audience"],
              claims: claims,
              expires: expirationDate,
              signingCredentials: signingCredentials);
    }

    private Claim[] CreateUserClaims(User user)
    {
        Claim[] claims =
        {
            new Claim(ClaimTypes.Email, user.Email),
        };

        return claims;
    }

    private SigningCredentials CreateSigningCredentials()
    {
        byte[] secretKeyEncoding = Encoding.UTF8.GetBytes(_configuration["Jwt:key"]!);
        SymmetricSecurityKey symmetricKey = new SymmetricSecurityKey(secretKeyEncoding);
        return new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256);
    }

    private DateTime CreateExpirationDate()
    {
        var hoursExpiration = double.Parse(_configuration["TokenConfiguration:ExpireHours"]!);
        return DateTime.Now.AddHours(hoursExpiration);
    }
}
