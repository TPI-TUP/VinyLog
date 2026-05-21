using Application.Models.Requests;

namespace Application.Interfaces;

public interface ICustomAuthenticationService
{
    Task<string> Autenticar(AuthenticationRequest authenticationRequest);
}