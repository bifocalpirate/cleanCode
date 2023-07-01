using BuberBuilder.Domain.Entities;

namespace BuberDinner.Application.Services.Authentication.Common;

public record AuthenticationResult(
    User User,
    string token
);