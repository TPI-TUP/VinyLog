using System;

namespace Domain.Entities;

public class Review
{
    public int Id { get; set; }
    public int IdUser { get; set; }
    public int IdAlbum { get; set; }
    public string Content { get; set; }
    public int Rating { get; set; }
}
