using Domain.Entities;
using Domain.Interfaces;

namespace Application.Interfaces;

public interface IReviewRepository
    : IRepositoryBase<Review>
{
    // Obtiene la reseña realizada por un usuario para un album específico
    // Para validar que un usuario no pueda reseñar el mismo album mas de una vez
    Task<Review?> GetByUserAndAlbumAsync(
    int userId,
    int albumId);

    // Obtiene todas las reseñas asociadas a un album.
    // Para recalcular el promedio de puntuaciones
    Task<List<Review>> GetByAlbumIdAsync(
        int albumId);
}