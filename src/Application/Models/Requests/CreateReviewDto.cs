namespace Application.Models.Requests;

public class CreateReviewDto
{
    public int AlbumId { get; set; }

    public string Content { get; set; }

    public int Rating { get; set; }
}