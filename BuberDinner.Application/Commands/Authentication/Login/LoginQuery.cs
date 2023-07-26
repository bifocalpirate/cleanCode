using BuberDinner.Application.Services.Authentication.Common;
using ErrorOr;
using MediatR;

namespace BuberDinner.Application.Commands.Authentication.Login;

public record LoginQuery(string Email,
                        string Password) : IRequest<ErrorOr<AuthenticationResult>>;
