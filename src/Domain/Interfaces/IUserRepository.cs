using Domain.Entities;

namespace Domain.Interfaces;

public interface IUserRepository : IRepositoryBase<User>
{
    Task<User?> GetUserByUsernameAsync(string username);
}