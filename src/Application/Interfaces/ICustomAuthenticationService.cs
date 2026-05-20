using Application.Models.Requests;

namespace Application.Interfaces;

public interface ICustomAuthenticationService
{
    string Autenticar(AuthenticationRequest autheticationRequest);
}