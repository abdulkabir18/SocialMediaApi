using Application.Dtos;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Host.Controllers
{
    [Route("api/[controller]/auth")]
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
            if (!result.Status) return Unauthorized(result);
            return Ok(new { result });
        }

        [HttpPost("verify-email")]
        public async Task<IActionResult> VerifyEmail([FromQuery] string email, [FromQuery] string code)
        {
            var result = await _userService.VerifyEmail(email, code);
            if (!result.Status) return BadRequest(new { result });
            return Ok(new { result });
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest forgotPassword)
        {
            var result = await _userService.ForgotPassword(forgotPassword);
            if (!result.Status) return NotFound(new { result });
            return Ok(new { result });
        }

        [HttpPost("reset-password")]
        [Authorize]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequestModel resetPassword)
        {
            var result = await _userService.ResetPassword(resetPassword);
            if (!result.Status) return Unauthorized(new { result });
            return Ok(new { result });
        }
    }
}