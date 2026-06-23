using Domain.Entities;
using Application.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class ArtistRepository : RepositoryBase<Artist>, IArtistRepository
{
    public ArtistRepository(ApplicationContext context)
        : base(context)
    {
    }
}