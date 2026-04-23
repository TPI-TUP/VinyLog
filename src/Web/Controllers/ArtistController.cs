using Domain.Entities;
using Application.Models.Requests;
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


    [HttpGet("{artistId}", Name = "GetArtist")]
    public ActionResult<Artist> GetUser(int artistId)
    {
        var artist = _artistService.GetArtist(artistId);

        if (artist is null)
            return NotFound();

        return Ok(artist);

    }

    [HttpPost]
    public ActionResult<Artist> CreateArtist(ArtistCreateRequest newArtist)
    {
        var createdArtist = _artistService.CreateArtist(newArtist);

        return CreatedAtRoute(
            "GetArtist",
            new
            {
                artistId = createdArtist.Id
            },
            createdArtist
        );
    }
}