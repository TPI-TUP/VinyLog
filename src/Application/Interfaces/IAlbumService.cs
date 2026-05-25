using Domain.Entities;

namespace Application.Interfaces;

public interface IAlbumService
{
    Task<List<Album>> GetAllAsync();

    Task<Album?> GetByIdAsync(int id);

    Task<Album> AddAsync(
        Album album,
        string artistName);

    Task UpdateAsync(Album album);

    Task DeleteAsync(int id);
}