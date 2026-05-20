using Domain.Entities;
using Application.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ArtistRepository : IArtistRepository
{
    private readonly ApplicationContext _context;

    public ArtistRepository(ApplicationContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Artist artist)
    {
        await _context.Artists.AddAsync(artist);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Artist>> ListAsync()
    {
        return await _context.Artists.ToListAsync();
    }

    public async Task<Artist?> GetByIdAsync(int id)
    {
        return await _context.Artists.FindAsync(id);
    }

    public async Task UpdateAsync(Artist artist)
    {
        _context.Artists.Update(artist);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Artist artist)
    {
        _context.Artists.Remove(artist);
        await _context.SaveChangesAsync();
    }
}