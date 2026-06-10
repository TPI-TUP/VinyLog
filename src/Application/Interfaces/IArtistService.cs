using Application.Models;
using Application.Models.Requests;

namespace Application.Interfaces;

public interface IArtistService
{
    Task<List<ArtistDto>> GetAllAsync();

    Task<ArtistDto?> GetByIdAsync(int id);

    Task<ArtistDto> CreateArtistAsync(CreateArtistDto dto);

    Task<ArtistDto?> UpdateArtistAsync(int id, UpdateArtistDto dto);

    Task DeleteArtistAsync(int id);
}
