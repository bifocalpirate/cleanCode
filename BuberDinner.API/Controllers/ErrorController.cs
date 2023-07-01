using Microsoft.AspNetCore.Mvc;

namespace BuberDinner.Api.Controller;

[ApiController]
public class ErrorController : ControllerBase
{
    [Route("/error")]
    public IActionResult Error(){
        return Problem();
    }
}