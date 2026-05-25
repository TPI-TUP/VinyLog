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

    public async Task<Album> CreateAsync(Album album, string artistName)
    {
        var videoId = await _youtubeService
            .SearchAlbumVideoAsync(
                album.Name,
                artistName);

        album.YoutubeVideoId = videoId;

        return await _albumRepository.CreateAsync(album);
    }
}