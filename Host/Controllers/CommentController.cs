﻿using Application.Dtos;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

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

        [SwaggerOperation(Description = "Customers Comment On A Post")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddComment([FromBody] AddCommentRequestModel model)
        {
            var result = await _commentService.CommentPost(model);

            if (!result.Status) return NoContent();

            return Ok(new { result });
        }

        [HttpGet("comment/{id}")]

        public async Task<IActionResult> ViewCommentPost([FromRoute] Guid id)
        {
            var result = await _commentService.ViewComment(id);

            if (!result.Status) return NotFound(result);

            return Ok(new { result });
        }

        [HttpGet("comments/{postId}")]

        public async Task<IActionResult> ViewCommentsPost([FromRoute] Guid postId)
        {
            var result = await _commentService.ViewAllComments(postId);

            if (!result.Status) return NotFound(result);

            return Ok(new { result });
        }

        [HttpPatch("edit")]
        [Authorize]
        public async Task<IActionResult> EditComment([FromBody] EditCommentRequestModel model)
        {
            var result = await _commentService.EditComment(model);
            if (!result.Status) return BadRequest(new { result }); return Ok(new { result });
        }

        [HttpDelete("delete/{model}")]
        [Authorize]
        public async Task<IActionResult> DeleteComment([FromRoute] DeleteCommentRequestModel model)
        {
            var result = await _commentService.DeleteComment(model);
            if (!result.Status) return BadRequest(new { result }); return Ok(new { result });
        }
    }
}
