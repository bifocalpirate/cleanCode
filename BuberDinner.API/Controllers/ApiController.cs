using Microsoft.AspNetCore.Mvc;
using BuberDinner.Contracts.Authentication;
using ErrorOr;
using BuberDinner.Application.Services.Authentication.Common;
using MediatR;
using BuberDinner.Application.Commands.Authentication.Register;
using BuberDinner.Application.Commands.Authentication.Login;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using BuberDinner.Api.Common.Http;

namespace BuberDinner.Api.Controller;

[ApiController]
public class ApiController : ControllerBase
{    
    protected IActionResult Problem(List<Error> errors)
    {
        if (errors.Count is 0){
            return Problem();
        }

        if (errors.All(error => error.Type == ErrorType.Validation))
        {
            return ValidationProblem(errors);
        }

        HttpContext.Items[HttpContextItemKeys.Errors] = errors;

        return Problem(errors[0]);
    }

    private IActionResult Problem(Error error)
    {
        
        var statusCode = error.Type switch
        {
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            _ => StatusCodes.Status500InternalServerError,
        };

        return Problem(statusCode: statusCode, title: error.Description);
    }

    private IActionResult ValidationProblem(List<Error> errors)
    {
        var modelStateDictionary = new ModelStateDictionary();
        foreach (var error in errors)
        {
            modelStateDictionary.AddModelError(
                error.Code,
                error.Description
            );
        }
        return ValidationProblem(modelStateDictionary);
    }
}