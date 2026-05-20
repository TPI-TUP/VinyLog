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

    public Artist CreateArtist(Artist artistRequest)
    {

        return _artistRepository.CreateArtist(artistRequest);
        // var newArtist = new Artist
        // {
        //     Name = artistRequest.Name,
        //     DateBirthday = artistRequest.DateBirthday,
        //     Country = artistRequest.Country,
        //     Description = artistRequest.Description
        // };

        // _context.Artists.Add(newArtist);
        // _context.SaveChanges();

        // return newArtist;
    }

    public List<Artist> GetAll()
    {
        return _artistRepository.GetAll();
    }

    public Artist? GetArtist(int id)
    {
        return _artistRepository.GetArtist(id);
    }

    public Artist? UpdateArtist(int id, Artist updatedArtist)
    {

        return _artistRepository.UpdateArtist(id, updatedArtist);
        // var artist = _context.Artists.FirstOrDefault(a => a.Id == id);

        // if (artist == null) return null;

        // artist.Name = updatedArtist.Name;
        // artist.DateBirthday = updatedArtist.DateBirthday;
        // artist.Country = updatedArtist.Country;
        // artist.Description = updatedArtist.Description;

        // _context.SaveChanges();

        // return artist;
    }

    public bool DeleteArtist(int id)
    {

        return _artistRepository.DeleteArtist(id);
        // var artist = _context.Artists.FirstOrDefault(a => a.Id == id);

        // if (artist == null) return false;

        // _context.Artists.Remove(artist);
        // _context.SaveChanges();
        // return true;
    }

}
