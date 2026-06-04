using System.ComponentModel.DataAnnotations;

namespace Application.Models.Requests;

public class CreateArtistDto
{
    [Required]
    public string Name { get; set; } = string.Empty;

    public DateTime DateBirthday { get; set; }

    [Required]
    public string Country { get; set; } = string.Empty;

    public string? Description { get; set; }
}
