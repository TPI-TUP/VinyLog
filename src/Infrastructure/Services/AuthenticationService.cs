using Application.Interfaces;
using Application.Models.Requests;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;

namespace Infrastructure.Services;

public class AutenticacionService
    : ICustomAuthenticationService
{
    private readonly IUserRepository _userRepository;

    private readonly AutenticacionServiceOptions _options;

    public AutenticacionService(
        IUserRepository userRepository,

        IOptions<AutenticacionServiceOptions> options)
    {
        _userRepository = userRepository;
        _options = options.Value;
    }


    // Metodo para validar User
    private async Task<User?> ValidateUser(
        AuthenticationRequest authenticationRequest)
    {
        // Buscar User en BBDD por Username
        var user =
           await _userRepository.GetUserByUsernameAsync(
                authenticationRequest.Username);

        if (user == null)
        {
            return null;
        }
        //  ¿¿ Validar Pw : es seguro de esta forma ??

        if (user.Password != authenticationRequest.Password)
        {
            return null;
        }

        return user;
    }
    // Metodo Autenticar: recibe Username y Pw y devuelve JWT
    public async Task<string> Autenticar(AuthenticationRequest authenticationRequest)
    {
        var user = await ValidateUser(authenticationRequest);

        if (user == null)
        {
            throw new UnauthorizedAccessException(
                "User authentication failed");
        }
        // Obtener secret key desde configuracion
        var secretKey = _options.SecretForKey ?? throw new InvalidOperationException("Authentication secret key no está configurada.");

        var securityPassword = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));

        var credentials =
            new SigningCredentials(
                securityPassword,
                SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new Claim(
                ClaimTypes.NameIdentifier,
                user.Id.ToString()),

            new Claim(
                ClaimTypes.Name,
                user.Username!),

            new Claim(
                ClaimTypes.Role,
                user.Role.ToString())
        };
        // Crear token
        var token =
            new JwtSecurityToken(
                issuer: _options.Issuer,
                audience: _options.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credentials);

        var tokenToReturn = new JwtSecurityTokenHandler()
                //Pasar el token a string
                .WriteToken(token);

        return tokenToReturn.ToString();
    }

    public class AutenticacionServiceOptions
    {
        public const string AutenticacionService = "AutenticacionService";

        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string SecretForKey { get; set; }
    }

}
