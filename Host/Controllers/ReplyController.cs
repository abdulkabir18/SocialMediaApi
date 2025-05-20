using System.Threading.Tasks;
using Application.Dtos;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReplyController : ControllerBase
    {
        private readonly IReplyService _replyService;
        public ReplyController(IReplyService replyService)
        {
            _replyService = replyService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddReply([FromBody] AddReplyRequestModel model)
        {
            var result = await _replyService.AddReply(model);
            if(result.Status)
            {
                return Ok(new { result });
            }

            return NoContent();
        }

        [HttpGet("reply/{id}")]
        public async Task<IActionResult> GetReply([FromRoute] Guid id)
        {
            var result = await _replyService.GetReply(id);

            if (!result.Status) return NotFound(new {result});

            return Ok(new { result });
        }

        [HttpGet("replies/{commentId}")]
        public async Task<IActionResult> GetReplies([FromRoute]Guid commentId)
        {
            var result = await _replyService.GetReplies(commentId);

            if (!result.Status) return NotFound(new {result});

            return Ok(new { result });
        }

        [HttpPatch("edit")]
        [Authorize]
        public async Task<IActionResult> EditReply([FromBody]  EditReplyRequestModel model)
        {
            var result = await _replyService.EditReply(model);
            if(!result.Status) return BadRequest(new {result}); return Ok(new { result });
        }

        [HttpDelete("delete/{model}")]
        [Authorize]
        public async Task<IActionResult> DeleteReply([FromRoute] DeleteReplyRequestModel model)
        {
            var result = await _replyService.DeleteReply(model);
            if(!result.Status) return BadRequest(new {result}); return Ok(new{result});
        }
    }
}
