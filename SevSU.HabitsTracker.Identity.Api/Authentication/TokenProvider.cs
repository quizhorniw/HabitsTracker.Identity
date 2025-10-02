using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using SevSU.HabitsTracker.Identity.Api.Models.Entities;
using SevSU.HabitsTracker.Identity.Api.Time;

namespace SevSU.HabitsTracker.Identity.Api.Authentication;

public class TokenProvider : ITokenProvider
{
    private readonly IConfiguration _configuration;
    private readonly IDateTimeProvider _dateTimeProvider;

    public TokenProvider(IConfiguration configuration, IDateTimeProvider dateTimeProvider)
    {
        _configuration = configuration;
        _dateTimeProvider = dateTimeProvider;
    }

    public string CreateAccessToken(User user)
    {
        var secretKey = _configuration["Jwt:Secret"]!;
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(
            [
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            ]),
            Expires = _dateTimeProvider.UtcNow.AddMinutes(_configuration.GetValue<int>("Jwt:AccessExpirationMinutes")),
            SigningCredentials = credentials,
            Issuer = _configuration["Jwt:Issuer"],
            Audience = _configuration["Jwt:Audience"]
        };

        var handler = new JsonWebTokenHandler();

        var token = handler.CreateToken(tokenDescriptor);
        return token;
    }

    public RefreshToken GenerateRefreshToken(Guid userId)
    {
        return new RefreshToken
        {
            UserId = userId,
            Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(128)),
            CreatedAt = _dateTimeProvider.UtcNow,
            ExpiresAt = _dateTimeProvider.UtcNow.AddDays(_configuration.GetValue<int>("Jwt:RefreshExpirationDays"))
        };
    }
}