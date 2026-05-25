using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class AlbumService
{
    private readonly IAlbumRepository _albumRepository;
    private readonly IYoutubeService _youtubeService;

    public AlbumService(
        IAlbumRepository albumRepository,
        IYoutubeService youtubeService)
    {
        _albumRepository = albumRepository;
        _youtubeService = youtubeService;
    }
    public async Task<List<Album>> GetAllAsync()
    {
        return await _albumRepository.ListAsync();
    }

    public async Task<Album?> GetByIdAsync(int id)
    {
        return await _albumRepository.GetByIdAsync(id);
    }

    public async Task<Album> AddAsync(Album album, string artistName)
    {
        var videoId = await _youtubeService
            .SearchAlbumVideoAsync(
                album.Name,
                artistName);

        album.YoutubeVideoId = videoId;

        return await _albumRepository.AddAsync(album);
    }
    public async Task UpdateAsync(Album album)
    {
        await _albumRepository.UpdateAsync(album);
    }

    public async Task DeleteAsync(int id)
    {
        var album = await _albumRepository.GetByIdAsync(id);

        if (album == null)
        {
            throw new Exception("Album not found");
        }

        await _albumRepository.DeleteAsync(album);
    }

}