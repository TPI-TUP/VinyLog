using Domain.Entities;

namespace Application.Interfaces;

public interface IArtistRepository
{
    Artist CreateArtist(Artist artist);

    List<Artist> GetAll();

    Artist? GetArtist(int id);

    Artist? UpdateArtist(int id, Artist artist);

    bool DeleteArtist(int id);
}