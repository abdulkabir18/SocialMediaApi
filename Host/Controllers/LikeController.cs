using Application.Dtos;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikeController : ControllerBase
    {
        private readonly ILikeService _likeService;

        public LikeController(ILikeService likeService)
        {
            _likeService = likeService;
        }

        [HttpPost]
        [Authorize]

        public async Task<IActionResult> LikePost(LikeRequestModel model)
        {
            var result = await _likeService.LikePost(model);

            if (!result.Status) return NoContent();

            return Ok(new { result });
        }

        [HttpGet("like/{id}")]
        public async Task<IActionResult> GetLike([FromRoute] Guid id)
        {
            var result = await _likeService.GetLike(id);

            if (!result.Status) return NotFound(result);

            return Ok(new { result });
        }

        [HttpGet("likes/{postId}")]
        public async Task<IActionResult> GetLikes([FromRoute] Guid postId)
        {
            var result = await _likeService.GetLikes(postId);

            if (!result.Status) return NotFound(result);

            return Ok(new { result });
        }
    }
}
