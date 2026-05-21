using Domain.Entities;

namespace Application.Interfaces;

public interface IArtistRepository
{
    Task AddAsync(Artist artist);
    Task<List<Artist>> ListAsync();
    Task<Artist?> GetByIdAsync(int id);
    Task UpdateAsync(Artist artist);
    Task DeleteAsync(Artist artist);
}