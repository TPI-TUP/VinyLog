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
    public ActionResult<List<Artist>> GetAll()
    {
        return Ok(_artistService.ListAsync());
    }
    [HttpGet("{artistId:int}", Name = "GetArtist")]
    public ActionResult<Artist> GetArtist([FromRoute] int artistId)
    {
        var artist = _artistService.GetByIdAsync(artistId);

        if (artist is null)
            return NotFound();

        return Ok(artist);
    }


    [Authorize]
    [HttpPost]
    public ActionResult<Artist> CreateArtist([FromBody] Artist artist)
    {

        var createdArtist = _artistService.AddAsync(artist);

        return CreatedAtRoute(
            "GetArtist",
            new { artistId = createdArtist.Id },
            createdArtist
        );
    }

    [HttpPut("{artistId:int}")]
    public ActionResult<Artist> UpdateArtist(int artistId, [FromBody] Artist artist)
    {
        var updated = _artistService.UpdateAsync(artistId, artist);

        if (updated is null)
            return NotFound();

        return Ok(updated);
    }

    [HttpDelete("{artistId:int}")]
    public ActionResult DeleteArtist(int artistId)
    {
        var deleted = _artistService.DeleteAsync(artistId);

        if (!deleted)
            return NotFound();

        return NoContent();
    }

}
