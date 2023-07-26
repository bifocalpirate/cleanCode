using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BuberBuilder.Domain.Entities;
using BuberDinner.Application.Common.Interfaces.Authentication;
using BuberDinner.Application.Common.Interfaces.Services;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;


namespace BuberDinner.Infrastructure.Authentication;
public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly JwtSettings _jwtSettings;

    public JwtTokenGenerator(IDateTimeProvider dateTimeProvider, IOptions<JwtSettings> settings)
    {
        this._dateTimeProvider = dateTimeProvider;
        this._jwtSettings = settings.Value;
    }
    public string GenerateToken(User user)
    {
        var signingCredentials  = new SigningCredentials(
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(this._jwtSettings.Secret ?? string.Empty)),
                SecurityAlgorithms.HmacSha256);
        
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub,user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName??string.Empty),
            new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName??string.Empty),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        var securityToken  =new JwtSecurityToken(
            issuer:this._jwtSettings.Issuer,
            audience:this._jwtSettings.Audience,
            expires:this._dateTimeProvider.UtcNow.AddMinutes(this._jwtSettings.ExpiryMinutes),
            claims:claims,
            signingCredentials:signingCredentials);
        return new JwtSecurityTokenHandler().WriteToken(securityToken);

    }
}
