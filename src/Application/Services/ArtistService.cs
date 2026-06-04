using Application.Interfaces;
using Application.Models;
using Application.Models.Requests;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class ArtistService : IArtistService
{
    private readonly IArtistRepository _artistRepository;

    public ArtistService(IArtistRepository artistRepository)
    {
        _artistRepository = artistRepository;
    }

    public async Task<List<ArtistDto>> GetAllAsync()
    {
        var artists = await _artistRepository.ListAsync();

        return artists
            .Select(ArtistDto.Create)
            .ToList();
    }

    public async Task<ArtistDto?> GetByIdAsync(int id)
    {
        var artist = await _artistRepository.GetByIdAsync(id);

        if (artist == null)
        {
            return null;
        }

        return ArtistDto.Create(artist);
    }

    public async Task<ArtistDto> CreateArtistAsync(CreateArtistDto dto)
    {
        var artist = new Artist
        {
            Name = dto.Name,
            DateBirthday = dto.DateBirthday,
            Country = dto.Country,
            Description = dto.Description
        };

        var createdArtist = await _artistRepository.AddAsync(artist);

        return ArtistDto.Create(createdArtist);
    }

    public async Task<ArtistDto?> UpdateArtistAsync(int id, UpdateArtistDto dto)
    {
        var artist = await _artistRepository.GetByIdAsync(id);

        if (artist == null)
        {
            return null;
        }

        artist.Name = dto.Name;
        artist.DateBirthday = dto.DateBirthday;
        artist.Country = dto.Country;
        artist.Description = dto.Description;

        await _artistRepository.UpdateAsync(artist);

        return ArtistDto.Create(artist);
    }

    public async Task<bool> DeleteArtistAsync(int id)
    {
        var artist = await _artistRepository.GetByIdAsync(id);

        if (artist == null)
        {
            return false;
        }

        await _artistRepository.DeleteAsync(artist);

        return true;
    }
}
