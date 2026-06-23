using Domain.Entities;

namespace Application.Models.Requests;

public class CreateUserDto
{
    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public Role Role { get; set; }
}