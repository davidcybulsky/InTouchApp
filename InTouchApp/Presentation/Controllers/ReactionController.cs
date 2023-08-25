using InTouchApi.Application.Interfaces.Reaction;
using InTouchApi.Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InTouchApi.Presentation.Controllers
{
    [ApiController]
    [Route("reaction")]
    public class ReactionController : ControllerBase
    {
        private readonly IReactionService _service;

        public ReactionController(IReactionService service)
        {
            _service = service;
        }

        [HttpPost("post/{postId}")]
        [Authorize]
        public async Task<ActionResult<int>> CreatePostReactionAsync([FromRoute] int postId, [FromBody] CreateReactionDto createReactionDto)
        {
            var reactionId = await _service.CreatePostReactionAsync(postId, createReactionDto);
            return StatusCode(StatusCodes.Status201Created, reactionId);
        }

        [HttpPost("comment/{commentId}")]
        [Authorize]
        public async Task<ActionResult<int>> CreateCommentReactionAsync([FromRoute] int commentId, [FromBody] CreateReactionDto createReactionDto)
        {
            var reactionId = await _service.CreateCommentReactionAsync(commentId, createReactionDto);
            return StatusCode(StatusCodes.Status201Created, reactionId);
        }

        [HttpPut("post/{reactionId}")]
        [Authorize]
        public async Task<ActionResult> UpdatePostReactionAsync([FromRoute] int reactionId, [FromBody] UpdateReactionDto updateReactionDto)
        {
            await _service.UpdatePostReactionAsync(reactionId, updateReactionDto);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        [HttpPut("comment/{reactionId}")]
        [Authorize]
        public async Task<ActionResult> UpdateCommentReactionAsync([FromRoute] int reactionId, [FromBody] UpdateReactionDto updateReactionDto)
        {
            await _service.UpdateCommentReactionAsync(reactionId, updateReactionDto);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        [HttpDelete("post/{reactionId}")]
        [Authorize]
        public async Task<ActionResult> DeletePostReactionAsync([FromRoute] int reactionId)
        {
            await _service.DeletePostReactionAsync(reactionId);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        [HttpDelete("comment/{reactionId}")]
        [Authorize]
        public async Task<ActionResult> DeleteCommentReactionAsync([FromRoute] int reactionId)
        {
            await _service.DeleteCommentReactionAsync(reactionId);
            return StatusCode(StatusCodes.Status204NoContent);
        }
    }
}
