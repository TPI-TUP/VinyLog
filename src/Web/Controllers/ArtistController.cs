using Application.Interfaces;
using Application.Models;
using Application.Models.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[ApiController]
[Route("api/artists")]
public class ArtistController : ControllerBase
{
    private readonly IArtistService _artistService;

    public ArtistController(IArtistService artistService)
    {
        _artistService = artistService;
    }
    //  GET ARTIST
    [HttpGet]
    public async Task<ActionResult<List<ArtistDto>>> GetAll()
    {
        return Ok(await _artistService.GetAllAsync());
    }

    // GET ARTIST BY ID
    [HttpGet("{artistId:int}", Name = "GetArtist")]
    public async Task<ActionResult<ArtistDto>> GetArtist([FromRoute] int artistId)
    {
        var artist = await _artistService.GetByIdAsync(artistId);

        return Ok(artist);
    }

    // POST ARTIST
    [Authorize]
    [HttpPost]
    public async Task<ActionResult<ArtistDto>> CreateArtist([FromBody] CreateArtistDto dto)
    {
        var createdArtist = await _artistService.CreateArtistAsync(dto);

        return CreatedAtRoute(
            "GetArtist",
            new { artistId = createdArtist.Id },
            createdArtist
        );
    }

    //  UPDATE ARTIST
    [Authorize]
    [HttpPut("{artistId:int}")]
    public async Task<ActionResult<ArtistDto>> UpdateArtist(int artistId, [FromBody] UpdateArtistDto dto)
    {
        var updated = await _artistService.UpdateArtistAsync(artistId, dto);

        return Ok(updated);
    }

    // DELETE ARTIST
    [Authorize]
    [HttpDelete("{artistId:int}")]
    public async Task<ActionResult> DeleteArtist(int artistId)
    {
        await _artistService.DeleteArtistAsync(artistId);

        return NoContent();
    }
}
