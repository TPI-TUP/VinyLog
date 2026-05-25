using Application.Interfaces;
using Application.Models.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[ApiController]
[Route("api/authentication")]

public class AuthenticationController : ControllerBase
{
    private readonly ICustomAuthenticationService _customAuthenticationService;

    public AuthenticationController(ICustomAuthenticationService authenticationService)
    {
        _customAuthenticationService = authenticationService;
    }

    [HttpPost("authenticate")]
    public async Task<IActionResult> Autenticar(AuthenticationRequest authenticationRequest)
    {
        string token = await _customAuthenticationService.Autenticar(authenticationRequest);

        return Ok(token);
    }

}