using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories;


public class ReviewRepository
    : RepositoryBase<Review>,
      IReviewRepository
{
    public ReviewRepository(
        ApplicationContext context)
        : base(context)
    {
    }
}