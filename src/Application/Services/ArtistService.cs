using Domain.Entities;

namespace Application.Services;

public class ArtistService
{
    private static List<Artist> _artistList = new List<Artist>();
    private static int _nextId = 1;
    public Artist CreateArtist(Artist artistRequest)
    {
        var newArtist = new Artist
        {
            Id = _nextId++,
            Name = artistRequest.Name,
            DateBirthday = artistRequest.DateBirthday,
            Country = artistRequest.Country,
            Description = artistRequest.Description
        };

        _artistList.Add(newArtist);

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
