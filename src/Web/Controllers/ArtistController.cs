using Domain.Entities;
using Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Web.Controllers;

[Route("api/artists")]
[ApiController]
public class ArtistController : ControllerBase
{

    private readonly ArtistService _artistService;

    public ArtistController(ArtistService artistService)
    {
        _artistService = artistService;
    }


    [HttpGet]
    public async Task<ActionResult<List<Artist>>> GetAll()
    {
        return Ok(await _artistService.GetAll());
    }
    [HttpGet("{artistId:int}", Name = "GetArtist")]
    public async Task<ActionResult<Artist>> GetArtist([FromRoute] int artistId)
    {
        var artist = await _artistService.GetArtist(artistId);

        if (artist is null)
            return NotFound();

        return Ok(artist);
    }


    [Authorize]
    [HttpPost]
    public async Task<ActionResult<Artist>> CreateArtist([FromBody] Artist artist)
    {

        var createdArtist = await _artistService.CreateArtist(artist);

        return CreatedAtRoute(
            "GetArtist",
            new { artistId = createdArtist.Id },
            createdArtist
        );
    }

    [HttpPut("{artistId:int}")]
    public async Task<ActionResult<Artist>> UpdateArtist(int artistId, [FromBody] Artist artist)
    {
        var updated = await _artistService.UpdateArtist(artistId, artist);

        if (updated is null)
            return NotFound();

        return Ok(updated);
    }

    [HttpDelete("{artistId:int}")]
    public async Task<ActionResult> DeleteArtist(int artistId)
    {
        var deleted = await _artistService.DeleteArtist(artistId);

        if (!deleted)
            return NotFound();

        return NoContent();
    }

}
