using BuberDinner.Application.Services.Authentication.Common;
using ErrorOr;
using MediatR;

namespace BuberDinner.Application.Commands.Authentication.Register;

public record RegisterCommand(string FirstName,
                        string LastName,
                        string Email,
                        string Password):IRequest<ErrorOr<AuthenticationResult>>;