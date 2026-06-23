using Application.Models;
using Application.Models.Requests;

namespace Application.Interfaces;

public interface IUserService
{
    Task<UserDto> CreateUserAsync(CreateUserDto userRequest);

    Task<List<UserDto>> GetAllAsync();

    Task<UserDto> GetUserAsync(int id);

    Task<UserDto> UpdateUserAsync(int id, UpdateUserDto updatedUser);

    Task DeleteUserAsync(int id);
}