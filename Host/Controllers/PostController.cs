using Application.Dtos;
using Application.Interfaces.CurrentUser;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly ICurrentUser _currentUser;

        public PostController(IPostService postService,ICurrentUser currentUser)
        {
            _postService = postService;
            _currentUser = currentUser;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreatePost([FromForm] MakePostRequestModel create)
        {
            var result = await _postService.CreatePost(create);

            if (!result.Status) return NoContent();

            return Ok(new { result });
        }

        [HttpGet("guid:id")]
        [Authorize]
        public async Task<IActionResult> ViewPost([FromRoute]Guid id)
        {
            var result = await _postService.ViewPost(id);

            if (!result.Status) return NotFound(result);

            return Ok(new { result });
        }

        [HttpGet("userposts/")]
        [Authorize]
        public async Task<IActionResult> ViewPosts()
        {
            var currentMediaUser = await _currentUser.GetCurrentMediaUser();
            if(currentMediaUser.Data != null)
            {
                var result = await _postService.ViewAllPosts(currentMediaUser.Data.Id);
                if (!result.Status) return NotFound(result);

                return Ok(new { result });
            }
            return BadRequest();
        }

        [HttpGet("posts/")]
        [Authorize]
        public async Task<IActionResult> ViewAllPosts()
        {
            var result = await _postService.ViewAllPosts();
            if (!result.Status) return NotFound(result);

            return Ok(new { result });
        }

        [HttpPatch("edit")]
        [Authorize]
        public async Task<IActionResult> EditPost([FromBody] EditPostRequestModel requestModel)
        {
            var result = await _postService.EditPost(requestModel);
            if(!result.Status) return BadRequest(new { result });

            return Ok(new { result });
        }

        [HttpDelete("delete")]
        [Authorize]
        public async Task<IActionResult> DeletePost([FromRoute] DeletePostRequestModel requestModel)
        {
            var result = await _postService.DeletePost(requestModel);
            if (!result.Status) return BadRequest(new { result });

            return Ok(new { result });
        }
    }
}
