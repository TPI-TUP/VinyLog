using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Exceptions;
using Application.Models;
using Application.Models.Requests;

namespace Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    // CREATE USER
    public async Task<UserDto> CreateUserAsync(CreateUserDto dto)
    {
        // Validaciones
        if (string.IsNullOrWhiteSpace(dto.Username))
        {
            throw new AppValidationException(
                "El nombre de usuario es obligatorio.");
        }
        if (string.IsNullOrWhiteSpace(dto.Email))
        {
            throw new AppValidationException(
                "El correo electrónico es obligatorio.");
        }
        if (dto.Email != null && !dto.Email.Contains("@"))
        {
            throw new AppValidationException(
                "El correo electrónico no tiene un formato válido.");
        }
        if (string.IsNullOrWhiteSpace(dto.Password))
        {
            throw new AppValidationException(
                "La contraseña es obligatoria.");
        }
        var newUser = new User
        {
            Username = dto.Username,
            Email = dto.Email,
            Password = dto.Password,
            Role = dto.Role
        };

        await _userRepository.AddAsync(newUser);

        return UserDto.Create(newUser);
    }

    //  GET ALL USERS
    public async Task<List<UserDto>> GetAllAsync()
    {
        var users = await _userRepository.ListAsync();

        return users
            .Select(UserDto.Create)
            .ToList();
    }

    // GET USER BY ID
    public async Task<UserDto> GetUserAsync(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);

        if (user == null)
        {
            throw new NotFoundException("User", id);
        }

        return UserDto.Create(user);
    }

    //  UPDATE USER
    public async Task<UserDto> UpdateUserAsync(
    int id,
    UpdateUserDto dto)
    {
        var user =
            await _userRepository.GetByIdAsync(id);

        if (user == null)
        {
            throw new NotFoundException("User", id);
        }

        if (dto.Username != null)
        {
            user.Username = dto.Username;
        }

        if (dto.Email != null)
        {
            user.Email = dto.Email;
        }

        if (dto.Role.HasValue)
        {
            user.Role = dto.Role.Value;
        }


        await _userRepository.UpdateAsync(user);

        return UserDto.Create(user);
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