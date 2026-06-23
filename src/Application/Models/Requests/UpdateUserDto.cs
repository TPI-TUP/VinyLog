using Domain.Entities;

namespace Application.Models.Requests;

public class UpdateUserDto
{
    public string? Username { get; set; }

    public string? Email { get; set; }

    public Role? Role { get; set; }
}