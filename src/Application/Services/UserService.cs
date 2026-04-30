using Domain.Entities;
using Infrastructure.Data;

namespace Application.Services;

public class UserService
{
    private readonly ApplicationContext _context;

    public UserService(ApplicationContext context)
    {
        _context = context;
    }

    public User CreateUser(User userRequest)
    {
        var newUser = new User
        {
            Username = userRequest.Username,
            Email = userRequest.Email,
            Password = userRequest.Password,
            Role = userRequest.Role
        };

        _context.Users.Add(newUser);
        _context.SaveChanges();

        return newUser;
    }

    public List<User> GetAll()
    {
        return _context.Users.ToList();
    }

    public User? GetUser(int id)
    {
        return _context.Users.FirstOrDefault(a => a.Id == id);
    }

    public User? UpdateUser(int id, User updatedUser)
    {
        var user = _context.Users.FirstOrDefault(a => a.Id == id);

        if (user == null) return null;

        user.Username = updatedUser.Username;
        user.Email = updatedUser.Email;
        user.Password = updatedUser.Password;
        user.Role = updatedUser.Role;

        _context.SaveChanges();

        return user;
    }

    public bool DeleteUser(int id)
    {
        var user = _context.Users.FirstOrDefault(a => a.Id == id);

        if (user == null) return false;

        _context.Users.Remove(user);
        _context.SaveChanges();
        return true;
    }

}
