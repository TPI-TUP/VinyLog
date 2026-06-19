using Domain.Entities;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Application.Models;
using Microsoft.AspNetCore.Authorization;

namespace Web.Controllers;

[Authorize(Roles = "Superadmin")]
[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{

    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    // GET USERS
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetAll()
    {
        var users = await _userService.GetAllAsync();

        var usersDto = users.Select(UserDto.Create);

        return Ok(usersDto);
    }

    //  GET USER BY ID
    [HttpGet("{userId:int}", Name = "GetUser")]
    public async Task<ActionResult<UserDto>> GetUser([FromRoute] int userId)
    {
        var user = await _userService.GetUserAsync(userId);

        return Ok(UserDto.Create(user));

    }

    // POST USER
    [AllowAnonymous]
    [HttpPost]
    public async Task<ActionResult<UserDto>> CreateUser([FromBody] User user)
    {
        var createdUser = await _userService.CreateUserAsync(user);

        return CreatedAtRoute(
            "GetUser",
            new
            {
                userId = createdUser.Id
            },
            UserDto.Create(createdUser)
        );
    }

    //  UPDATE USER
    [HttpPut("{userId:int}")]
    public async Task<ActionResult<UserDto>> UpdateUser(int userId, [FromBody] User user)
    {
        var updated = await _userService.UpdateUserAsync(
            userId,
            user);

        return Ok(UserDto.Create(updated));
    }

    // DELETE USER
    [HttpDelete("{userId:int}")]
    public async Task<ActionResult> DeleteUser(int userId)
    {
        await _userService.DeleteUserAsync(userId);

        return NoContent();
    }

}
