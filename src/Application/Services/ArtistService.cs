using Domain.Entities;

namespace Application.Services;

public class ArtistService
{
    private static List<Artist> _artistList = new List<Artist>();

    public Artist CreateArtist(Artist artistCreateRequest)
    {
        var newArtist = new Artist();

        newArtist.Name = artistCreateRequest.Name;
        newArtist.DateBirthday = artistCreateRequest.DateBirthday;
        newArtist.Country = artistCreateRequest.Country;
        newArtist.Description = artistCreateRequest.Description;

        _artistList.Add(newArtist);

        return newArtist;
    }
}
