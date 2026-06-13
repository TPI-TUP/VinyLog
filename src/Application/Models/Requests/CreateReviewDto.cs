namespace Application.Models.Requests;

public class CreateReviewDto
{
    public int IdAlbum { get; set; }

    public string Content { get; set; }

    public int Rating { get; set; }
}