using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using Application.Models;
using Application.Models.Requests;
using Domain.Exceptions;

namespace Application.Services;

public class AlbumService : IAlbumService
{
    private readonly IAlbumRepository _albumRepository;
    private readonly IYoutubeService _youtubeService;
    private readonly IArtistRepository _artistRepository;

    public AlbumService(
        IAlbumRepository albumRepository,
        IYoutubeService youtubeService,
        IArtistRepository artistRepository)
    {
        _albumRepository = albumRepository;
        _youtubeService = youtubeService;
        _artistRepository = artistRepository;
    }

    // GET ALL ALBUMS
    public async Task<List<AlbumDto>> GetAllAsync()
    {
        var albums = await _albumRepository.ListAsync();

        return albums
            .Select(AlbumDto.Create)
            .ToList();
    }

    // GET BY ID ALBUMS
    public async Task<AlbumDto?> GetByIdAsync(int id)
    {
        var album = await _albumRepository.GetByIdAsync(id);

        if (album == null)
        {
            throw new NotFoundException("Album", id);
        }

        return AlbumDto.Create(album);
    }

    // CREATE ALBUM 
    public async Task<AlbumDto> AddAsync(CreateAlbumDto dto)

    {
        // Validaciones 
        if (string.IsNullOrWhiteSpace(dto.Name))
        {
            throw new AppValidationException("El nombre del álbum es obligatorio.");
        }

        if (dto.ArtistIds.Count == 0)
        {
            throw new AppValidationException("Debe seleccionar al menos un artista.");
        }

        if (dto.ReleaseDate > DateTime.UtcNow)
        {
            throw new AppValidationException("La fecha de lanzamiento no puede ser futura.");
        }
        var album = new Album
        {
            Name = dto.Name,
            Genre = dto.Genre,
            ReleaseDate = dto.ReleaseDate,
            Description = dto.Description,
            Image = dto.Image,
            //ArtistName = dto.ArtistName
        };

        foreach (var artistId in dto.ArtistIds)
        {
            var artist = await _artistRepository.GetByIdAsync(artistId);
            if (artist == null)
            {
                throw new NotFoundException("Artist", artistId);
            }
            album.Artists.Add(artist);
        }

        var firstArtist = await _artistRepository
            .GetByIdAsync(dto.ArtistIds.First());

        var videoId = await _youtubeService
            .SearchAlbumVideoAsync(
                album.Name,
                firstArtist?.Name ?? "");

        album.YoutubeVideoId = videoId;
        //album.YoutubeVideoId = null;

        var createdAlbum = await _albumRepository
            .AddAsync(album);

        return AlbumDto.Create(createdAlbum);
    }

    // UPDATE ALBUM
    public async Task UpdateAsync(int id, UpdateAlbumDto dto)
    {
        var album = await _albumRepository
        .GetByIdAsync(id);

        if (album == null)
        {
            throw new NotFoundException("Album", id);
        }

        album.Name = dto.Name;
        album.Genre = dto.Genre;
        album.ReleaseDate = dto.ReleaseDate;
        album.Description = dto.Description;
        album.Image = dto.Image;
        //album.ArtistName = dto.ArtistName;
        album.Artists.Clear();
        foreach (var artistId in dto.ArtistIds)
        {
            var artist = await _artistRepository.GetByIdAsync(artistId);
            if (artist == null)
            {
                throw new NotFoundException("Artist", artistId);
            }
            album.Artists.Add(artist);
        }

        await _albumRepository.UpdateAsync(album);
    }


    // DELETE ALBUM
    public async Task DeleteAsync(int id)
    {
        var album = await _albumRepository.GetByIdAsync(id);

        if (album == null)
        {
            throw new NotFoundException("Album", id);
        }

        await _albumRepository.DeleteAsync(album);
    }

}