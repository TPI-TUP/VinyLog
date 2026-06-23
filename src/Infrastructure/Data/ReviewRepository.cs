using Domain.Entities;
using Application.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;


public class ReviewRepository
    : RepositoryBase<Review>,
      IReviewRepository
{
    private readonly ApplicationContext _context;

    public ReviewRepository(
    ApplicationContext context)
    : base(context)
    {
        _context = context;
    }
    public async Task<Review?> GetByUserAndAlbumAsync(
        int userId,
        int albumId)
    {
        // Busca si existe una reseña de ese Usuario para ese Album
        return await _context.Reviews
            .FirstOrDefaultAsync(r =>
                r.UserId == userId &&
                r.AlbumId == albumId);
    }

    public async Task<List<Review>> GetByAlbumIdAsync(
        int albumId)
    {
        // Recupera todas las reseñas de un album.
        // Para calcular y actualizar el promedio del rating del album
        return await _context.Reviews
            .Where(r => r.AlbumId == albumId)
            .ToListAsync();
    }
}