using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Artist> Artists { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Album> Albums { get; set; }

        public DbSet<Review> Reviews { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(
            ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Crea un indice unico compuesto por IdUser+IdAlbum para la tabla Reviews
            //  Evita que un mismo usuario haga mas de una reseña por album
            modelBuilder.Entity<Review>()
                .HasIndex(r => new
                {
                    r.UserId,
                    r.AlbumId
                })
                .IsUnique();
        }
    }
}