using Domain.Entities;

namespace Application.Models;

public class ReviewDto
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int AlbumId { get; set; }

    public string Content { get; set; }

    public int Rating { get; set; }

    public static ReviewDto Create(Review review)
    {
        return new ReviewDto
        {
            Id = review.Id,
            UserId = review.UserId,
            AlbumId = review.AlbumId,
            Content = review.Content,
            Rating = review.Rating
        };
    }
}