using Application.Dtos;
using Application.Interfaces.CurrentUser;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MediaUserController : ControllerBase
    {
        private readonly IMediaUserService _mediaUserService;
        private readonly ICurrentUser _currentUser;
        public MediaUserController(IMediaUserService mediaUserService, ICurrentUser currentUser)
        {
            _mediaUserService = mediaUserService;
            _currentUser = currentUser;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUpAccount([FromForm] RegisterRequestModel register)
        {
            var result = await _mediaUserService.RegisterUser(register);

            if (!result.Status)
            {
                return BadRequest(result);
            }
            return Ok(new { result });
        }

        [HttpPatch("edit")]
        [Authorize]
        public async Task<IActionResult> EditAccount([FromForm] EditRequestModel edit)
        {
            var result = await _mediaUserService.EditDetails(edit);

            if (!result.Status) return BadRequest(new { result });

            return Ok(new { result });
        }

        [HttpDelete("delete")]
        [Authorize]
        public async Task<IActionResult> DeleteAccount()
        {
            var mediaUser = await _currentUser.GetCurrentMediaUser();
            var delete = new DeleteAccountRequestModel { MediaUserId = mediaUser.Data!.Id };
            var result = await _mediaUserService.DeleteAccount(delete);

            if (!result.Status) return BadRequest(new { result });

            return Ok(new { result });
        }

        [HttpGet("currentMediaUser")]
        [Authorize]
        public async Task<IActionResult> GetCurrentMediaUser()
        {
            var result = await _currentUser.GetCurrentMediaUser();
            if (!result.Status)
            {
                return BadRequest(result);
            }
            return Ok(new { result });
        }
    }
}
