using System;

namespace Domain.Entities;

public class User
{
    public int Id { get; set; }
    public string? Username { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public Role Role { get; set; }

    // Relacion: 1-N (un usuario puede reseñar varios albums)
    //  Por ejemplo: user.Reviews devuelve todas las reviews que hizo el usuario 
    public ICollection<Review> Reviews { get; set; }
    = new List<Review>();
}

public enum Role
{
    User,
    Admin,
    Superadmin
}