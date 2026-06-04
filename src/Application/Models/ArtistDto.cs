using Domain.Entities;

namespace Application.Models;

public class ArtistDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public DateTime DateBirthday { get; set; }
    public string Country { get; set; } = string.Empty;
    public string? Description { get; set; }

    public static ArtistDto Create(Artist artist)
    {
        var dto = new ArtistDto();
        dto.Id = artist.Id;
        dto.Name = artist.Name;
        dto.DateBirthday = artist.DateBirthday;
        dto.Country = artist.Country;
        dto.Description = artist.Description;

        return dto;
    }
}
