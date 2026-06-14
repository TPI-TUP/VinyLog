using System;

namespace Domain.Entities;

public class Review
{
    public int Id { get; set; }

    public int IdUser { get; set; }
    // Propiedad de navegacion: permite que EF entienda las relaciones entre entidades. No crea columnas nuevas, solo le dicen a EF que la columna IdUser apunta a la tabla Users
    public User User { get; set; }

    public int IdAlbum { get; set; }
    public Album Album { get; set; }

    public string Content { get; set; }

    public int Rating { get; set; }
}
