using Application.Interfaces;
using Application.Models;
using Application.Models.Requests;
using Domain.Entities;
using Domain.Exceptions;

namespace Application.Services;

public class ArtistService : IArtistService
{
    private readonly IArtistRepository _artistRepository;

    public ArtistService(IArtistRepository artistRepository)
    {
        _artistRepository = artistRepository;
    }

    // GET ALL ARTIST
    public async Task<List<ArtistDto>> GetAllAsync()
    {
        var artists = await _artistRepository.ListAsync();

        return artists
            .Select(ArtistDto.Create)
            .ToList();
    }

    // GET BY ID ARTIST
    public async Task<ArtistDto?> GetByIdAsync(int id)
    {
        var artist = await _artistRepository.GetByIdAsync(id);

        if (artist == null)
        {
            throw new NotFoundException("Artist", id);
        }

        return ArtistDto.Create(artist);
    }

    // CREATE ARTIST
    public async Task<ArtistDto> CreateArtistAsync(CreateArtistDto dto)
    {
        // Validaciones
        if (string.IsNullOrWhiteSpace(dto.Name))
        {
            throw new AppValidationException(
                "El nombre del artista es obligatorio.");
        }
        if (string.IsNullOrWhiteSpace(dto.Country))
        {
            throw new AppValidationException(
                "El país del artista es obligatorio.");
        }
        if (dto.DateBirthday > DateTime.UtcNow)
        {
            throw new AppValidationException(
                "La fecha de nacimiento no puede ser futura.");
        }
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

    // UPDATE ARTIST
    public async Task<ArtistDto?> UpdateArtistAsync(int id, UpdateArtistDto dto)
    {
        var artist = await _artistRepository.GetByIdAsync(id);

        if (artist == null)
        {
            throw new NotFoundException("Artist", id); ;
        }

        artist.Name = dto.Name;
        artist.DateBirthday = dto.DateBirthday;
        artist.Country = dto.Country;
        artist.Description = dto.Description;

        await _artistRepository.UpdateAsync(artist);

        return ArtistDto.Create(artist);
    }
    //  DELELTE ARTIST
    public async Task DeleteArtistAsync(int id)
    {
        var artist = await _artistRepository.GetByIdAsync(id);

        if (artist == null)
        {
            throw new NotFoundException("Artist", id);
        }

        await _artistRepository.DeleteAsync(artist);

    }
}
