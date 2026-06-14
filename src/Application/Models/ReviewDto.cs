using Domain.Entities;

namespace Application.Models;

public class ReviewDto
{
    public int Id { get; set; }

    public int IdUser { get; set; }

    public int IdAlbum { get; set; }

    public string Content { get; set; }

    public int Rating { get; set; }

    public static ReviewDto Create(Review review)
    {
        return new ReviewDto
        {
            Id = review.Id,
            IdUser = review.UserId,
            IdAlbum = review.AlbumId,
            Content = review.Content,
            Rating = review.Rating
        };
    }
}