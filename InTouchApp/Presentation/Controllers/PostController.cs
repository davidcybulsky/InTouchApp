using InTouchApi.Application.Interfaces;
using InTouchApi.Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InTouchApi.Presentation.Controllers
{
    [Route("post")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _service;

        public PostController(IPostService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostDto>>> GetAllPostsAsync()
        {
            var postDtos = await _service.GetAllPostsAsync();
            return StatusCode(200, postDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PostDto>> GetPostByIdAsync([FromRoute] int id)
        {
            var postDto = _service.GetPostByIdAsync(id);
            return StatusCode(200, postDto);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<int>> CreatePostAsync([FromBody] CreatePostDto createPostDto)
        {
            var id = _service.CreatePostAsync(createPostDto);
            return StatusCode(201, id);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatePostAsync([FromRoute] int id, [FromBody] UpdatePostDto updatePostDto)
        {
            await _service.UpdatePostAsync(id, updatePostDto);
            return StatusCode(204);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePostAsync([FromRoute] int id)
        {
            await _service.DeletePostAsync(id);
            return StatusCode(204);
        }
    }
}
