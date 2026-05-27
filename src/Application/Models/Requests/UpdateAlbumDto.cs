namespace Application.Models.Requests;

public class UpdateAlbumDto
{
    public string Name { get; set; }

    public string Genre { get; set; }

    public DateTime ReleaseDate { get; set; }

    public string Description { get; set; }

    public string Image { get; set; }

    public string ArtistName { get; set; }
}