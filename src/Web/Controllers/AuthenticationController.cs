using Domain.Entities;
using Application.Services;
using Microsoft.AspNetCore.Mvc;
using Application.Models;
using Application.Interfaces;

namespace Web.Controllers;

[Route("api/authentication")]
[ApiController]

public class AuthenticationController : ControllerBase
{
    private readonly ICustomAuthenticationService _customAuthenticationService;

    public AuthenticationController(IConfiguration config, ICustomAuthenticationService authenticationService)
    {
        _customAuthenticationService = authenticationService;
    }

    [HttpPost("authenticate")]
    public ActionResult<string> Autenticar(AuthenticationRequest authenticationRequest)
    {
        string token = _customAuthenticationService.Autenticar(authenticationRequest);

        return Ok(token);
    }



}