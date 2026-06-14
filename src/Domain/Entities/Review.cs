using System;

namespace Domain.Entities;

public class Review
{
    public int Id { get; set; }

    public int UserId { get; set; }
    // Propiedad de navegacion: permite que EF entienda las relaciones entre entidades Review y User
    public User User { get; set; }

    public int AlbumId { get; set; }
    public Album Album { get; set; }

    public string Content { get; set; }

    public int Rating { get; set; }
}
