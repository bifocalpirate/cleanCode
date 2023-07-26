// <copyright file="DinnersController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace BuberDinner.Api.Controller;

using Microsoft.AspNetCore.Mvc;


[Route("[controller]")]

public class DinnersController : ApiController
{
    [HttpGet]
    public IActionResult ListDinners()
    {
        return Ok(Array.Empty<string>());
    }

    private IActionResult Ok(string[] strings)
    {
        throw new NotImplementedException();
    }
}
