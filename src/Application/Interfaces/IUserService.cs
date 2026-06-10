using Domain.Entities;

namespace Application.Interfaces;

public interface IUserService
{
    Task<User> CreateUserAsync(User userRequest);

    Task<List<User>> GetAllAsync();

    Task<User> GetUserAsync(int id);

    Task<User> UpdateUserAsync(int id, User updatedUser);

    Task DeleteUserAsync(int id);
}