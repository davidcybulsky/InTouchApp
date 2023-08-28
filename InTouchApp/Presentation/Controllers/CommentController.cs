using InTouchApi.Application.Interfaces;
using InTouchApi.Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InTouchApi.Presentation.Controllers
{
    [ApiController]
    [Route("comment")]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _service;

        public CommentController(ICommentService service)
        {
            _service = service;
        }

        [HttpPost("post/{postId}")]
        [Authorize]
        public async Task<ActionResult<int>> CreatePostCommentAsync([FromRoute] int postId, [FromBody] CreateCommentDto createCommentDto)
        {
            var commentId = await _service.CreatePostCommentAsync(postId, createCommentDto);
            return StatusCode(StatusCodes.Status201Created, commentId);
        }

        [HttpPut("post/{commentId}")]
        [Authorize]
        public async Task<ActionResult> UpdatePostCommentAsync([FromRoute] int commentId, [FromBody] UpdateCommentDto updateCommentDto)
        {
            await _service.UpdatePostCommentAsync(commentId, updateCommentDto);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        [HttpDelete("post/{commentId}")]
        [Authorize]
        public async Task<ActionResult> DeletePostCommentAsync([FromRoute] int commentId)
        {
            await _service.DeletePostCommentAsync(commentId);
            return StatusCode(StatusCodes.Status204NoContent);
        }
    }
}
