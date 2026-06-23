using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Application.Models;
using Microsoft.AspNetCore.Authorization;
using Application.Models.Requests;

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

        return Ok(users);
    }

    //  GET USER BY ID
    [HttpGet("{userId:int}", Name = "GetUser")]
    public async Task<ActionResult<UserDto>> GetUser([FromRoute] int userId)
    {
        var user = await _userService.GetUserAsync(userId);

        return Ok(user);

    }

    // POST USER
    [AllowAnonymous]
    [HttpPost]
    public async Task<ActionResult<UserDto>> CreateUser([FromBody] CreateUserDto dto)
    {
        var createdUser = await _userService.CreateUserAsync(dto);

        return CreatedAtRoute(
            "GetUser",
            new
            {
                userId = createdUser.Id
            },
            createdUser);
    }

    //  UPDATE USER
    [HttpPut("{userId:int}")]
    public async Task<ActionResult<UserDto>> UpdateUser(int userId, [FromBody] UpdateUserDto dto)
    {
        var updated = await _userService.UpdateUserAsync(
            userId,
            dto);

        return Ok(updated);
    }

    // DELETE USER
    [HttpDelete("{userId:int}")]
    public async Task<ActionResult> DeleteUser(int userId)
    {
        await _userService.DeleteUserAsync(userId);

        return NoContent();
    }

}
