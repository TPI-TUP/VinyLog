using Domain.Entities;
using Infrastructure.Data;

namespace Application.Services;

public class ArtistService
{
    private readonly ApplicationContext _context;

    public ArtistService(ApplicationContext context)
    {
        _context = context;
    }

    public Artist CreateArtist(Artist artistRequest)
    {
        var newArtist = new Artist
        {
            Name = artistRequest.Name,
            DateBirthday = artistRequest.DateBirthday,
            Country = artistRequest.Country,
            Description = artistRequest.Description
        };

        _context.Artists.Add(newArtist);
        _context.SaveChanges();

        return newArtist;
    }

    public List<Artist> GetAll()
    {
        return _artistList;
    }

    public Artist? GetArtist(int id)
    {
        return _artistList.FirstOrDefault(a => a.Id == id);
    }

    public Artist? UpdateArtist(int id, Artist updatedArtist)
    {
        var artist = _artistList.FirstOrDefault(a => a.Id == id);

        if (artist == null) return null;

        artist.Name = updatedArtist.Name;
        artist.DateBirthday = updatedArtist.DateBirthday;
        artist.Country = updatedArtist.Country;
        artist.Description = updatedArtist.Description;

        return artist;
    }

    public bool DeleteArtist(int id)
    {
        var artist = _artistList.FirstOrDefault(a => a.Id == id);

        if (artist == null) return false;

        _artistList.Remove(artist);
        return true;
    }

}
