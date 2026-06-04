namespace Application.Models.Requests;

public class UpdateArtistDto
{
    public string? Name { get; set; }

    public DateTime DateBirthday { get; set; }

    public string Country { get; set; } = string.Empty;

    public string? Description { get; set; }
}
