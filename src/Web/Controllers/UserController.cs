using Domain.Entities;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[Route("api/users")]
[ApiController]
public class UserController : ControllerBase
{

    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public ActionResult<List<User>> GetAll()
    {
        return Ok(_userService.GetAll());
    }
    [HttpGet("{userId:int}", Name = "GetUser")]
    public ActionResult<User> GetUser([FromRoute] int userId)
    {
        var user = _userService.GetUser(userId);

        if (user is null)
            return NotFound();

        return Ok(user);

    }

    [HttpPost]
    public ActionResult<User> CreateUser([FromBody] User user)
    {
        var createdUser = _userService.CreateUser(user);

        return CreatedAtRoute(
            "GetUser",
            new
            {
                userId = createdUser.Id
            },
            createdUser
        );
    }


    [HttpPut("{userId:int}")]
    public ActionResult<User> UpdateUser(int userId, [FromBody] User user)
    {
        var updated = _userService.UpdateUser(userId, user);

        if (updated is null)
            return NotFound();

        return Ok(updated);
    }

    [HttpDelete("{userId:int}")]
    public ActionResult DeleteUser(int userId)
    {
        var deleted = _userService.DeleteUser(userId);

        if (!deleted)
            return NotFound();

        return NoContent();
    }

}