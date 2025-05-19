using Application.Dtos;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserRequestModel loginUser)
        {
            var result = await _userService.Login(loginUser);
            if (!result.Status)
            {
                return Unauthorized(result);
            }
            return Ok(new { result });
        }
    }
}
