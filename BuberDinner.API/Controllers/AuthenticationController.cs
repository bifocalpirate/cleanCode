using Microsoft.AspNetCore.Mvc;
using BuberDinner.Contracts.Authentication;
using ErrorOr;
using BuberDinner.Application.Services.Authentication.Common;
using MediatR;
using BuberDinner.Application.Commands.Authentication.Register;
using BuberDinner.Application.Commands.Authentication.Login;

namespace BuberDinner.Api.Controller;

[ApiController]
[Route("auth")]
public class AuthenticationController : ControllerBase
{    
    private readonly ISender _mediator;
    public AuthenticationController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var command = new RegisterCommand(request.FirstName, 
                        request.LastName,
                        request.Email,
                        request.Password);

        ErrorOr<AuthenticationResult> registerResult = await _mediator.Send(command);

        return registerResult.MatchFirst(
           authResult => Ok(MapAuthResults(authResult)),
            firstError => Problem(statusCode: StatusCodes.Status409Conflict, title: firstError.Description));
    }    

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var command =  new LoginQuery(request.Email, request.Password);
        ErrorOr<AuthenticationResult> authResponse = await _mediator.Send(command);

        return authResponse.MatchFirst(
            authResults => Ok(MapAuthResults(authResults)),
            firstError => Problem(statusCode:StatusCodes.Status401Unauthorized,title:firstError.Description)
        );
    }
    private static AuthenticationResponse MapAuthResults(AuthenticationResult authResult)
    {
        return new AuthenticationResponse(
                    authResult.User.Id,
                    authResult.User.FirstName,
                    authResult.User.LastName,
                    authResult.User.Email,
                    authResult.token);
    }
}