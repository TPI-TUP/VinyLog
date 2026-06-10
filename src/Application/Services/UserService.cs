using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Exceptions;

namespace Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    // CREATE USER
    public async Task<User> CreateUserAsync(User userRequest)
    {
        // Validaciones
        if (string.IsNullOrWhiteSpace(userRequest.Username))
        {
            throw new AppValidationException(
                "El nombre de usuario es obligatorio.");
        }
        if (string.IsNullOrWhiteSpace(userRequest.Email))
        {
            throw new AppValidationException(
                "El correo electrónico es obligatorio.");
        }
        if (!userRequest.Email.Contains("@"))
        {
            throw new AppValidationException(
                "El correo electrónico no tiene un formato válido.");
        }
        if (string.IsNullOrWhiteSpace(userRequest.Password))
        {
            throw new AppValidationException(
                "La contraseña es obligatoria.");
        }
        var newUser = new User
        {
            Username = userRequest.Username,
            Email = userRequest.Email,
            Password = userRequest.Password,
            Role = userRequest.Role
        };

        await _userRepository.AddAsync(newUser);

        return newUser;
    }

    //  GET ALL USERS
    public async Task<List<User>> GetAllAsync()
    {
        return await _userRepository.ListAsync();
    }

    // GET USER BY ID
    public async Task<User> GetUserAsync(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);

        if (user == null)
        {
            throw new NotFoundException("User", id);
        }

        return user;
    }

    //  UPDATE USER
    public async Task<User> UpdateUserAsync(
    int id,
    User updatedUser)
    {
        var user =
            await _userRepository.GetByIdAsync(id);

        if (user == null)
        {
            throw new NotFoundException("User", id);
        }

        user.Username = updatedUser.Username;
        user.Email = updatedUser.Email;
        user.Password = updatedUser.Password;
        user.Role = updatedUser.Role;

        await _userRepository.UpdateAsync(user);

        return user;
    }

    //  DELETE USER
    public async Task DeleteUserAsync(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);

        if (user == null)
        {
            throw new NotFoundException("User", id);
        }

        await _userRepository.DeleteAsync(user);

    }
}