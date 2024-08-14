using DOTNETDemo.Constants;
using DOTNETDemo.Models.Request;
using DOTNETDemo.Services.UserService;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace DOTNETDemo.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet(ApiRoutes.User.GetUser)]
        public async Task<ActionResult> GetUserAsync([Required] int id)
        {
            var user = await _userService.GetUserAsyncById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost(ApiRoutes.User.CreateUserCard)]
        public async Task<ActionResult> PostUserDataAsync(UserCardRequest request)
        {
            var result = await _userService.PostUserDataAsync(request);
            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete(ApiRoutes.User.DeleteUserCard)]
        public async Task<ActionResult> DeleteUserAsync([Required] int id)
        {
            var success = await _userService.DeleteUserAsync(id);
            if (!success)
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpPut(ApiRoutes.User.UpdateUserCard)]
        public async Task<ActionResult> UpdateUserAsync([FromBody] UserCardRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _userService.UpdateUserAsync(request);
            if (result)
            {
                return Ok();
            }

            return NotFound();
        }
    }
}
