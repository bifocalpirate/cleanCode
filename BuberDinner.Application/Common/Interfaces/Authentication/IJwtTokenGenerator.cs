using BuberBuilder.Domain.Entities;

namespace BuberDinner.Application.Coomon.Interfaces.Authentication;

public interface IJwtTokenGenerator{
    string GenerateToken(User user);
}