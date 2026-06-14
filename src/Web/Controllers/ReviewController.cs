using Application.Interfaces;
using Application.Models;
using Application.Models.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Web.Controllers;

[ApiController]
[Route("api/reviews")]
public class ReviewController : ControllerBase
{
    private readonly IReviewService _reviewService;

    public ReviewController(
        IReviewService reviewService)
    {
        _reviewService = reviewService;
    }

    // GET ALL REVIEWS
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ReviewDto>>> GetAll()
    {
        var reviews =
            await _reviewService.GetAllAsync();

        return Ok(reviews);
    }

    // GET REVIEW BY ID
    [HttpGet("{id:int}")]
    public async Task<ActionResult<ReviewDto>> GetById(
        int id)
    {
        var review =
            await _reviewService.GetByIdAsync(id);

        return Ok(review);
    }

    // CREATE REVIEW
    [Authorize]
    [HttpPost]
    public async Task<ActionResult<ReviewDto>> Create(
        [FromBody] CreateReviewDto dto)
    {
        var userIdClaim =
            User.FindFirst(
                ClaimTypes.NameIdentifier);

        var userId =
            int.Parse(userIdClaim!.Value);

        var review =
            await _reviewService.AddAsync(
                dto,
                userId);

        return Ok(review);
    }

    // UPDATE REVIEW
    [Authorize]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        int id,
        [FromBody] UpdateReviewDto dto)
    {
        var userId =
        int.Parse(
            User.FindFirst(
                ClaimTypes.NameIdentifier)!
            .Value);
        await _reviewService.UpdateAsync(
            id,
            dto,
            userId);

        return NoContent();
    }

    // DELETE REVIEW
    [Authorize]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(
        int id)
    {

        var userId =
        int.Parse(
            User.FindFirst(
                ClaimTypes.NameIdentifier)!
            .Value);
        await _reviewService.DeleteAsync(id,
        userId);

        return NoContent();
    }
}