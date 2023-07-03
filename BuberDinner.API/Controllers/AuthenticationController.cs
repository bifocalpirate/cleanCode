using Microsoft.AspNetCore.Mvc;
using BuberDinner.Contracts.Authentication;
using ErrorOr;
using BuberDinner.Application.Services.Authentication.Common;
using MediatR;
using BuberDinner.Application.Commands.Authentication.Register;
using BuberDinner.Application.Commands.Authentication.Login;
using MapsterMapper;

namespace BuberDinner.Api.Controller;

[Route("auth")]
public class AuthenticationController : ApiController
{    
    private readonly ISender _mediator;
    private readonly IMapper _mapper;
    public AuthenticationController(ISender mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var command = _mapper.Map<RegisterCommand>(request);

        ErrorOr<AuthenticationResult> registerResult = await _mediator.Send(command);

        return registerResult.Match(
           authResult => Ok(_mapper.Map<AuthenticationResponse>(registerResult.Value)),
            errors => Problem(errors));
    }    

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var command =  _mapper.Map<LoginQuery>(request);
        ErrorOr<AuthenticationResult> authResponse = await _mediator.Send(command);
        
        return authResponse.Match(
            authResults => Ok(_mapper.Map<AuthenticationResponse>(authResponse.Value)),
            errors => Problem(errors));
    }    
}