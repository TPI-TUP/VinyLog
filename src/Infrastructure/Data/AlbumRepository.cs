using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class AlbumRepository
    : RepositoryBase<Album>, IAlbumRepository
{
    private readonly ApplicationContext _context;
    public AlbumRepository(ApplicationContext context)
        : base(context)
    {
        _context = context;
    }

    public override async Task<Album?> GetByIdAsync<TId>(
        TId id,
        CancellationToken cancellationToken = default)
    {
        return await _context.Albums
            .Include(a => a.Artists)
            .Include(a => a.Reviews)
            .FirstOrDefaultAsync(
                a => a.Id.Equals(id), 
                cancellationToken);
    }

    public override async Task<List<Album>> ListAsync(
        CancellationToken cancellationToken = default)
    {
        return await _context.Albums
            .Include(a => a.Artists)
            .Include(a => a.Reviews)
            .ToListAsync(cancellationToken);
    }
}