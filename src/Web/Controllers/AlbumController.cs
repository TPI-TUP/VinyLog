using Application.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;
using Application.Models;

namespace Web.Controllers;

[ApiController]
[Route("api/albums")]
public class AlbumController : ControllerBase
{
    private readonly IAlbumService _albumService;

    public AlbumController(IAlbumService albumService)
    {
        _albumService = albumService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var albums = await _albumService.GetAllAsync();

        var dtos = albums
        .Select(AlbumDto.Create)
        .ToList();

        return Ok(albums);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var album = await _albumService.GetByIdAsync(id);

        if (album == null)
        {
            return NotFound();
        }

        return Ok(AlbumDto.Create(album));
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] Album album)
    {
        var createdAlbum = await _albumService
            .AddAsync(album);

        return Ok(createdAlbum);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        int id,
        [FromBody] Album album)
    {
        album.Id = id;

        await _albumService.UpdateAsync(album);

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _albumService.DeleteAsync(id);

        return NoContent();
    }
}