using BuberDinner.Application.Common.Interfaces.Persistence;
using BuberDinner.Application.Coomon.Interfaces.Authentication;
using BuberDinner.Application.Services.Authentication.Common;
using ErrorOr;
using BuberDinner.Domain.Common.Errors;
using MediatR;
using BuberBuilder.Domain.Entities;
using BuberDinner.Application.Commands.Authentication.Login;

namespace BuberDinner.Application.Commands.Authentication.Register;

public class LoginQueryHandler : IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private IUserRepository _userRepository;
    public LoginQueryHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery command, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        if (_userRepository.GetUserByEmail(command.Email) is not User user)
        {
            return Errors.Authentication.InvalidCredentials;
        }

        if (user.Password != command.Password)
        {
            return Errors.Authentication.InvalidCredentials;
        }

        var token = _jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(
            user,
            token);
    }    
}