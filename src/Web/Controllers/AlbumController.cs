using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;
using Application.Models.Requests;
using Microsoft.AspNetCore.Authorization;

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

    // GET ALBUMS
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var albums = await _albumService.GetAllAsync();

        return Ok(albums);
    }

    // GET ALBUMS BY ID
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var album = await _albumService.GetByIdAsync(id);

        return Ok((album));
    }

    // POST ALBUM
    [Authorize(Roles = "Admin,Superadmin")]
    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateAlbumDto dto)
    {
        var createdAlbum = await _albumService
       .AddAsync(dto);

        return Ok(createdAlbum);
    }

    //  UPDATE ALBUM 
    [Authorize(Roles = "Admin,Superadmin")]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        int id,
        [FromBody] UpdateAlbumDto dto)
    {
        await _albumService.UpdateAsync(id, dto);

        return NoContent();
    }

    //  DELETE ALBUM
    [Authorize(Roles = "Admin,Superadmin")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _albumService.DeleteAsync(id);

        return NoContent();
    }
}
