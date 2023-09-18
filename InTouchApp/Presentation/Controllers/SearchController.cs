using InTouchApi.Application.Interfaces;
using InTouchApi.Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace InTouchApi.Presentation.Controllers
{
    [ApiController]
    [Route("search")]
    public class SearchController : ControllerBase
    {
        private readonly ISearchService _service;

        public SearchController(ISearchService service)
        {
            _service = service;
        }

        [HttpGet("user/{pattern}")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsersAsync([FromRoute] string pattern)
        {
            var users = await _service.GetUsersAsync(pattern);
            return StatusCode(StatusCodes.Status200OK, users);
        }

        [HttpGet("post/{pattern}")]
        public async Task<ActionResult<IEnumerable<PostDto>>> GetPostsAsync([FromRoute] string pattern)
        {
            var posts = await _service.GetPostsAsync(pattern);
            return StatusCode(StatusCodes.Status200OK, posts);
        }
    }
}
