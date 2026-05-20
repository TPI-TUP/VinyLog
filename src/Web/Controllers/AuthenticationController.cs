using Application.Interfaces;
using Application.Models.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[Route("api/authentication")]
[ApiController]

public class AuthenticationController : ControllerBase
{
    private readonly IConfiguration _config;
    private readonly ICustomAuthenticationService _customAuthenticationService;

    public AuthenticationController(IConfiguration config, ICustomAuthenticationService authenticationService)
    {
        _config = config;
        _customAuthenticationService = authenticationService;
    }

    [HttpPost("authenticate")]
    public ActionResult<string> Autenticar(AuthenticationRequest authenticationRequest)
    {
        string token = _customAuthenticationService.Autenticar(authenticationRequest);

        return Ok(token);
    }

}