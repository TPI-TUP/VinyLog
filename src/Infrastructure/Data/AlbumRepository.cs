using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class AlbumRepository
    : RepositoryBase<Album>, IAlbumRepository
{
    public AlbumRepository(ApplicationContext context)
        : base(context)
    {
    }
}