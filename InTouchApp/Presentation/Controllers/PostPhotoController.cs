using InTouchApi.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InTouchApi.Presentation.Controllers
{
    [ApiController]
    [Route("photo/post")]
    public class PostPhotoController : ControllerBase
    {
        private readonly IPostPhotoService _service;

        public PostPhotoController(IPostPhotoService service)
        {
            _service = service;
        }

        [HttpPost("{postId}")]
        public async Task<ActionResult> AddPostPhotoAsync([FromRoute] int postId, IFormFile file)
        {
            var photoDto = await _service.AddPostPhotoAsync(postId, file);
            return StatusCode(StatusCodes.Status201Created, photoDto);
        }

        [HttpDelete("{photoId}")]
        public async Task<ActionResult> DeletePostPhotoAsync([FromRoute] int photoId)
        {
            await _service.DeletePostPhotoAsync(photoId);
            return StatusCode(StatusCodes.Status204NoContent);
        }
    }
}
