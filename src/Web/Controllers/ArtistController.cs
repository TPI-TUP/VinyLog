using Domain.Entities;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

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
        return Ok(_artistService.GetAll());
    }
    [HttpGet("{artistId:int}", Name = "GetArtist")]
    public ActionResult<Artist> GetArtist([FromRoute] int artistId)
    {
        var artist = _artistService.GetArtist(artistId);

        if (artist is null)
            return NotFound();

        return Ok(artist);
    }



    [HttpPost]
    public ActionResult<Artist> CreateArtist([FromBody] Artist artist)
    {

        var createdArtist = _artistService.CreateArtist(artist);

        return CreatedAtRoute(
            "GetArtist",
            new { artistId = createdArtist.Id },
            createdArtist
        );
    }

    [HttpPut("{artistId:int}")]
    public ActionResult<Artist> UpdateArtist(int artistId, [FromBody] Artist artist)
    {
        var updated = _artistService.UpdateArtist(artistId, artist);

        if (updated is null)
            return NotFound();

        return Ok(updated);
    }

    [HttpDelete("{artistId:int}")]
    public ActionResult DeleteArtist(int artistId)
    {
        var deleted = _artistService.DeleteArtist(artistId);

        if (!deleted)
            return NotFound();

        return NoContent();
    }

}
