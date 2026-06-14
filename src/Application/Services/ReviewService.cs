using Application.Interfaces;
using Application.Models;
using Application.Models.Requests;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;

namespace Application.Services;

public class ReviewService : IReviewService
{
    private readonly IReviewRepository _reviewRepository;
    private readonly IAlbumRepository _albumRepository;
    private readonly IUserRepository _userRepository;

    public ReviewService(
        IReviewRepository reviewRepository,
        IAlbumRepository albumRepository,
        IUserRepository userRepository)
    {
        _reviewRepository = reviewRepository;
        _albumRepository = albumRepository;
        _userRepository = userRepository;
    }

    // GET ALL REVIEWS
    public async Task<List<ReviewDto>> GetAllAsync()
    {
        var reviews = await _reviewRepository.ListAsync();

        return reviews
            .Select(ReviewDto.Create)
            .ToList();
    }

    // GET REVIEW BY ID
    public async Task<ReviewDto?> GetByIdAsync(int id)
    {
        var review = await _reviewRepository.GetByIdAsync(id);

        if (review == null)
        {
            throw new NotFoundException("Review", id);
        }

        return ReviewDto.Create(review);
    }

    // CREATE REVIEW
    public async Task<ReviewDto> AddAsync(
        CreateReviewDto dto,
        int userId)
    {
        if (string.IsNullOrWhiteSpace(dto.Content))
        {
            throw new AppValidationException(
                "El contenido de la reseña no puede estar vacío.");
        }

        if (dto.Rating < 1 || dto.Rating > 5)
        {
            throw new AppValidationException(
                "La puntuación debe estar entre 1 y 5.");
        }

        var user = await _userRepository.GetByIdAsync(userId);

        if (user == null)
        {
            throw new NotFoundException("User", userId);
        }

        var album = await _albumRepository.GetByIdAsync(dto.AlbumId);

        if (album == null)
        {
            throw new NotFoundException("Album", dto.AlbumId);
        }

        var existingReview =
            await _reviewRepository.GetByUserAndAlbumAsync(
                userId,
                dto.AlbumId);

        if (existingReview != null)
        {
            throw new AppValidationException(
                "Ya realizaste una reseña para este álbum.");
        }

        var review = new Review
        {
            UserId = userId,
            AlbumId = dto.AlbumId,
            Content = dto.Content,
            Rating = dto.Rating
        };

        var createdReview =
            await _reviewRepository.AddAsync(review);

        await UpdateAlbumAverageRating(dto.AlbumId);

        return ReviewDto.Create(createdReview);
    }

    // UPDATE REVIEW
    public async Task UpdateAsync(
        int id,
        UpdateReviewDto dto,
        int userId)
    {
        var review =
            await _reviewRepository.GetByIdAsync(id);

        if (review == null)
        {
            throw new NotFoundException("Review", id);
        }

        if (review.UserId != userId)
        {
            throw new UnauthorizedAccessException(
                "No tienes permiso para modificar esta reseña.");
        }

        if (dto.Content != null)
        {
            if (string.IsNullOrWhiteSpace(dto.Content))
            {
                throw new AppValidationException(
                "El contenido de la reseña no puede estar vacío.");

            }
            review.Content = dto.Content;
        }

        if (dto.Rating.HasValue)
        {
            if (dto.Rating < 1 || dto.Rating > 5)
            {
                throw new AppValidationException(
                    "La puntuación debe estar entre 1 y 5.");
            }

            review.Rating = dto.Rating.Value;
        }

        await _reviewRepository.UpdateAsync(review);

        await UpdateAlbumAverageRating(review.AlbumId);
    }

    // DELETE REVIEW
    public async Task DeleteAsync(int id, int userId)
    {
        var review =
            await _reviewRepository.GetByIdAsync(id);

        if (review == null)
        {
            throw new NotFoundException("Review", id);
        }

        if (review.UserId != userId)
        {
            throw new UnauthorizedAccessException(
                "No tienes permiso para eliminar esta reseña.");
        }

        var albumId = review.AlbumId;

        await _reviewRepository.DeleteAsync(review);

        await UpdateAlbumAverageRating(albumId);
    }

    // RECALCULA ALBUM AVERAGE RATING
    private async Task UpdateAlbumAverageRating(
        int albumId)
    {
        var album =
            await _albumRepository.GetByIdAsync(albumId);

        if (album == null)
        {
            return;
        }

        var reviews =
            await _reviewRepository.GetByAlbumIdAsync(albumId);

        album.AverageRating = reviews.Any()
            ? Math.Round(
                reviews.Average(r => r.Rating),
                 1)
            : 0;

        await _albumRepository.UpdateAsync(album);
    }
}