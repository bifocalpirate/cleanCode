// <copyright file="AuthenticationController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace BuberDinner.Api.Controller;

using BuberDinner.Application.Commands.Authentication.Login;
using BuberDinner.Application.Commands.Authentication.Register;
using BuberDinner.Application.Services.Authentication.Common;
using BuberDinner.Contracts.Authentication;
using ErrorOr;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


[Route("auth")]
[AllowAnonymous]
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
           authResult => Ok(_mapper.Map<AuthenticationResponse>(registerResult.Value)), errors => Problem(errors));
    }    

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var command =  _mapper.Map<LoginQuery>(request);
        ErrorOr<AuthenticationResult> authResponse = await this._mediator.Send(command);
        
        return authResponse.Match(
            authResults => Ok(_mapper.Map<AuthenticationResponse>(authResponse.Value)),
            errors => Problem(errors));
    }    
}

