using Application.Models;
using Application.Models.Requests;

namespace Application.Interfaces;

public interface IReviewService
{
    Task<List<ReviewDto>> GetAllAsync();

    Task<ReviewDto?> GetByIdAsync(int id);

    // Task<List<ReviewDto>> GetByAlbumAsync(int albumId);

    Task<ReviewDto> AddAsync(
        CreateReviewDto dto,
        int userId);

    Task UpdateAsync(
        int id,
        UpdateReviewDto dto,
        int userId);

    Task DeleteAsync(int id,
        int userId);
}