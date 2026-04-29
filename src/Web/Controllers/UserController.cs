// using Microsoft.AspNetCore.Mvc;

// namespace User.Web.Controllers;

// [Route("api/users")]
// [ApiController]
// public class UserController : ControllerBase
// {

//     [HttpGet("{userId}", Name = "GetUser")]
//     public ActionResult<UserDto> GetUser(int userId)
//     {
//         var user = _userService.GetUser(userId);

//         if (user is null)
//             return NotFound();

//         return Ok(user);

//     }

//     [HttpPost]
//     public ActionResult<UserDto> CreateUser(UserCreateRequest newUser)
//     {
//         var createdUser = _userService.CreateUser(newUser);

//         return CreatedAtRoute(
//             "GetUser",
//             new
//             {
//                 userId = createdUser.Id
//             },
//             createdUser
//         );
//     }
// }