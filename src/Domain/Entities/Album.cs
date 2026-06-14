namespace Domain.Entities;

public class Album
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Genre { get; set; }

    public DateTime ReleaseDate { get; set; }

    public string Description { get; set; }

    public string Image { get; set; }

    public string? YoutubeVideoId { get; set; }

    public double AverageRating { get; set; }

    // public int IdArtist { get; set; }
    public string ArtistName { get; set; }

    // Relacion: 1-N (un album puede tener muchas reseñas)
    //  Por ejemplo: album.Reviews devuelve todas las reseñas de ese album
    public ICollection<Review> Reviews { get; set; }
        = new List<Review>();

}
