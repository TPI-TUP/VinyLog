using Application.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;
using Application.Models;
using Application.Models.Requests;

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

        return Ok(albums);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var album = await _albumService.GetByIdAsync(id);

        // if (album == null)
        // {
        //     return NotFound();
        // }

        return Ok((album));
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateAlbumDto dto)
    {
        var createdAlbum = await _albumService
       .AddAsync(dto);

        return Ok(createdAlbum);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        int id,
        [FromBody] UpdateAlbumDto dto)
    {
        await _albumService.UpdateAsync(id, dto);

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _albumService.DeleteAsync(id);

        return NoContent();
    }
}