using Domain.Entities;

namespace Application.Models;

public class AlbumDto
{
    public int Id { get; set; }
    public string Name { get; set; }

    public string Genre { get; set; }

    public DateTime ReleaseDate { get; set; }

    public string Description { get; set; }

    public string Image { get; set; }

    public string? YoutubeVideoId { get; set; }

    public double AverageRating { get; set; }

    public int IdArtist { get; set; }

    public static AlbumDto Create(Album album)
    {
        var dto = new AlbumDto();
        dto.Id = album.Id;
        dto.Name = album.Name;
        dto.Genre = album.Genre;
        dto.ReleaseDate = album.ReleaseDate;
        dto.Description = album.Description;
        dto.Image = album.Image;
        dto.YoutubeVideoId = album.YoutubeVideoId;
        dto.AverageRating = album.AverageRating;
        dto.IdArtist = album.IdArtist;

        return dto;
    }
}