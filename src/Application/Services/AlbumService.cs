using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using Application.Models;
using Application.Models.Requests;

namespace Application.Services;

public class AlbumService : IAlbumService
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
    public async Task<List<AlbumDto>> GetAllAsync()
    {
        var albums = await _albumRepository.ListAsync();

        return albums
            .Select(AlbumDto.Create)
            .ToList();
    }

    public async Task<AlbumDto?> GetByIdAsync(int id)
    {
        var album = await _albumRepository.GetByIdAsync(id);

        if (album == null)
        {
            return null;
        }

        return AlbumDto.Create(album);
    }

    public async Task<AlbumDto> AddAsync(CreateAlbumDto dto)
    {
        var album = new Album
        {
            Name = dto.Name,
            Genre = dto.Genre,
            ReleaseDate = dto.ReleaseDate,
            Description = dto.Description,
            Image = dto.Image,
            ArtistName = dto.ArtistName
        };

        var videoId = await _youtubeService
            .SearchAlbumVideoAsync(
                album.Name,
                album.ArtistName);

        album.YoutubeVideoId = videoId;

        var createdAlbum = await _albumRepository
        .AddAsync(album);

        return AlbumDto.Create(createdAlbum);
    }
    public async Task UpdateAsync(int id,
    UpdateAlbumDto dto)
    {
        var album = await _albumRepository
        .GetByIdAsync(id);

        if (album == null)
        {
            throw new Exception("Album not found");
        }

        album.Name = dto.Name;
        album.Genre = dto.Genre;
        album.ReleaseDate = dto.ReleaseDate;
        album.Description = dto.Description;
        album.Image = dto.Image;
        album.ArtistName = dto.ArtistName;

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