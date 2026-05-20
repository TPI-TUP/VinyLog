using Application.Interfaces;
using Application.Models.Requests;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Services;

public class AutenticacionService
    : ICustomAuthenticationService
{
    private readonly IUserRepository _userRepository;

    private const string SECRET_KEY =
        "ESTA_ES_UNA_CLAVE_SUPER_SECRETA_123456";

    public AutenticacionService(
        IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    private User? ValidateUser(
        AuthenticationRequest authenticationRequest)
    {
        var user =
            _userRepository.GetUserByUsername(
                authenticationRequest.Username);

        if (user == null)
        {
            return null;
        }

        if (user.Password != authenticationRequest.Password)
        {
            return null;
        }

        return user;
    }

    public string Autenticar(AuthenticationRequest authenticationRequest)
    {
        var user = ValidateUser(authenticationRequest);

        if (user == null)
        {
            throw new Exception(
                "User authentication failed");
        }

        var key =
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(SECRET_KEY));

        var credentials =
            new SigningCredentials(
                key,
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

        var token =
            new JwtSecurityToken(
                issuer: "VinyLogAPI",
                audience: "VinyLogUsers",
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credentials);

        return new JwtSecurityTokenHandler()
            .WriteToken(token);
    }
}