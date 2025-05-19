using Application.Dtos;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpPost]
        [Authorize]

        public async Task<IActionResult> AddComment([FromBody] AddCommentRequestModel model)
        {
            var result = await _commentService.CommentPost(model);

            if (!result.Status) return NoContent();

            return Ok(new { result });
        }

        [HttpGet("comment/{id}")]

        public async Task<IActionResult> GetCommentPost([FromRoute] Guid id)
        {
            var result = await _commentService.ViewComment(id);

            if (!result.Status) return NotFound(result);

            return Ok(new { result });
        }

        [HttpGet("comments/{postId}")]

        public async Task<IActionResult> GetCommentsPost([FromRoute] Guid postId)
        {
            var result = await _commentService.ViewAllComments(postId);

            if (!result.Status) return NotFound(result);

            return Ok(new { result });
        }
    }
}
