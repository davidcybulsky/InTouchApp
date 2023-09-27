using InTouchApi.Application.Interfaces.Reaction;
using InTouchApi.Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InTouchApi.Presentation.Controllers
{
    [ApiController]
    [Authorize]
    [Route("reaction")]
    public class ReactionController : ControllerBase
    {
        private readonly IReactionService _service;

        public ReactionController(IReactionService service)
        {
            _service = service;
        }

        [HttpPost("post/{postId}")]
        public async Task<ActionResult> CreatePostReactionAsync([FromRoute] int postId, [FromBody] CreateReactionDto createReactionDto)
        {
            await _service.CreatePostReactionAsync(postId, createReactionDto);
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPost("comment/{commentId}")]
        public async Task<ActionResult> CreateCommentReactionAsync([FromRoute] int commentId, [FromBody] CreateReactionDto createReactionDto)
        {
            await _service.CreateCommentReactionAsync(commentId, createReactionDto);
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPut("post/{postId}")]
        public async Task<ActionResult> UpdatePostReactionAsync([FromRoute] int postId, [FromBody] UpdateReactionDto updateReactionDto)
        {
            await _service.UpdatePostReactionAsync(postId, updateReactionDto);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        [HttpPut("comment/{commentId}")]
        public async Task<ActionResult> UpdateCommentReactionAsync([FromRoute] int commentId, [FromBody] UpdateReactionDto updateReactionDto)
        {
            await _service.UpdateCommentReactionAsync(commentId, updateReactionDto);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        [HttpDelete("post/{postId}")]
        public async Task<ActionResult> DeletePostReactionAsync([FromRoute] int postId)
        {
            await _service.DeletePostReactionAsync(postId);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        [HttpDelete("comment/{commentId}")]
        public async Task<ActionResult> DeleteCommentReactionAsync([FromRoute] int commentId)
        {
            await _service.DeleteCommentReactionAsync(commentId);
            return StatusCode(StatusCodes.Status204NoContent);
        }
    }
}
