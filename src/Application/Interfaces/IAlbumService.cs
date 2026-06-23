using Application.Models;
using Application.Models.Requests;

namespace Application.Interfaces;

public interface IAlbumService
{
    Task<List<AlbumDto>> GetAllAsync();

    Task<AlbumDto?> GetByIdAsync(int id);

    Task<AlbumDto> AddAsync(
        CreateAlbumDto dto);

    Task UpdateAsync(int id, UpdateAlbumDto dto);

    Task DeleteAsync(int id);
}