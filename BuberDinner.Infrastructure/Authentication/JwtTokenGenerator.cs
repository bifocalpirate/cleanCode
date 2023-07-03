namespace BuberDinner.Infrastructure.Authentication;

using System;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using BuberDinner.Application.Coomon.Interfaces.Authentication;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using BuberDinner.Application.Common.Interfaces.Services;
using Microsoft.Extensions.Options;
using BuberBuilder.Domain.Entities;

public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly JwtSettings _jwtSettings;

    public JwtTokenGenerator(IDateTimeProvider dateTimeProvider, IOptions<JwtSettings> settings)
    {
        _dateTimeProvider = dateTimeProvider;
        _jwtSettings = settings.Value;
    }
    public string GenerateToken(User user)
    {
        var signingCredentials  = new SigningCredentials(
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_jwtSettings.Secret??"")),
                    SecurityAlgorithms.HmacSha256);
        
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub,user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName??string.Empty),
            new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName??string.Empty),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var securityToken  =new JwtSecurityToken(
            issuer:_jwtSettings.Issuer,
            audience:_jwtSettings.Audience,
            expires:_dateTimeProvider.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes),
            claims:claims,
            signingCredentials:signingCredentials);
        return new JwtSecurityTokenHandler().WriteToken(securityToken);

    }
}