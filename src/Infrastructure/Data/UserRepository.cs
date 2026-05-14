using Domain.Entities;
using Domain.Interfaces;

namespace Infrastructure.Data;

public class UserRepository : EfRepository<User>, IUserRepository
{
    public UserRepository(ApplicationContext context) : base(context)
    {
    }

    public User? GetUserByUsername(string username)
    {
        return _context.Users.SingleOrDefault(p => p.Username == username);
    }
}