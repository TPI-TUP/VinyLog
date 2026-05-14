using Domain.Entities;

namespace Application.Models;

public class UserDto
{
    public int Id {get; set;}
    public string? Username {get; set;}
    public string? Email {get; set;}

    public static UserDto Create(User user)
    {
        var dto = new UserDto();
        dto.Id = user.Id;
        dto.Username = user.Username;
        dto.Email = user.Email;

        return dto;
    }
}