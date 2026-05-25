using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
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

        _userRepository.CreateAsync(newUser).Wait();

        return newUser;
    }

    public List<User> GetAll()
    {
        return _userRepository
            .ListAsync()
            .Result;
    }

    public User? GetUser(int id)
    {
        return _userRepository
            .GetByIdAsync(id)
            .Result;
    }

    public User? UpdateUser(
        int id,
        User updatedUser)
    {
        var user =
            _userRepository
                .GetByIdAsync(id)
                .Result;

        if (user == null)
        {
            return null;
        }

        user.Username = updatedUser.Username;
        user.Email = updatedUser.Email;
        user.Password = updatedUser.Password;
        user.Role = updatedUser.Role;

        _userRepository.UpdateAsync(user).Wait();

        return user;
    }

    public bool DeleteUser(int id)
    {
        var user =
            _userRepository
                .GetByIdAsync(id)
                .Result;

        if (user == null)
        {
            return false;
        }

        _userRepository.DeleteAsync(user).Wait();

        return true;
    }
}