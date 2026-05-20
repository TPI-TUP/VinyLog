using Application.Interfaces;
using Domain.Entities;


namespace Application.Services;

public class ArtistService
{
    private readonly IArtistRepository _artistRepository;

    public ArtistService(IArtistRepository artistRepository)
    {
        _artistRepository = artistRepository;
    }

    public async Task<Artist> CreateArtist(Artist artistRequest)
    {
        await _artistRepository.AddAsync(artistRequest);
        return artistRequest;
    }

    public async Task<List<Artist>> GetAll()
    {
        return await _artistRepository.ListAsync();
    }

    public async Task<Artist?> GetArtist(int id)
    {
        return await _artistRepository.GetByIdAsync(id);
    }

    public async Task<Artist?> UpdateArtist(int id, Artist updatedArtist)
    {
        var artist = await _artistRepository.GetByIdAsync(id);
        if (artist == null) return null;

        artist.Name = updatedArtist.Name;
        artist.DateBirthday = updatedArtist.DateBirthday;
        artist.Country = updatedArtist.Country;
        artist.Description = updatedArtist.Description;

        await _artistRepository.UpdateAsync(artist);
        return artist;
    }

    public async Task<bool> DeleteArtist(int id)
    {
        var artist = await _artistRepository.GetByIdAsync(id);
        if (artist == null) return false;

        await _artistRepository.DeleteAsync(artist);
        return true;
    }

}
