using Domain.Entities;

namespace Application.Interfaces;

public interface IUserService
{
    User CreateUser(User userRequest);

    List<User> GetAll();

    User? GetUser(int id);

    User? UpdateUser(int id, User updatedUser);

    bool DeleteUser(int id);
}