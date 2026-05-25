using Application.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[ApiController]
[Route("api/albums")]
public class AlbumController : ControllerBase
{
    private readonly AlbumService _albumService;

    public AlbumController(AlbumService albumService)
    {
        _albumService = albumService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var albums = await _albumService.GetAllAsync();

        return Ok(albums);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var album = await _albumService.GetByIdAsync(id);

        if (album == null)
        {
            return NotFound();
        }

        return Ok(album);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] Album album)
    {
        var createdAlbum = await _albumService
            .AddAsync(album, "Nirvana");

        return Ok(createdAlbum);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
        int id,
        [FromBody] Album album)
    {
        if (id != album.Id)
        {
            return BadRequest();
        }

        await _albumService.UpdateAsync(album);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _albumService.DeleteAsync(id);

        return NoContent();
    }
}