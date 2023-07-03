using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BuberDinner.Api.Middleware;

public class ErrorHandlingFilterAttribute:ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        if (context.Exception is null){
            return;
        }
        context.Result = new ObjectResult(new {
           error = "An error occurred while processing your request."
        })
        {
            StatusCode=500
        };

        context.ExceptionHandled = true;
    }
}
