using InTouchApi.Application.Interfaces;
using InTouchApi.Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace InTouchApi.Presentation.Controllers
{
    [ApiController]
    [Route("photo/user")]
    public class UserPhotoController : ControllerBase
    {
        private readonly IUserPhotoService _service;

        public UserPhotoController(IUserPhotoService service)
        {
            _service = service;
        }

        [HttpPost()]
        public async Task<ActionResult<IncludePhotoDto>> AddPhotoAsync(IFormFile file)
        {
            var photo = await _service.AddUserPhotoAsync(file);
            return StatusCode(StatusCodes.Status201Created, photo);
        }

        [HttpPut("{photoId}")]
        public async Task<ActionResult> SetAsMainAsync([FromRoute] int photoId)
        {
            await _service.SendAsMainUserPhotoAsync(photoId);
            return StatusCode(StatusCodes.Status202Accepted);
        }

        [HttpDelete("{photoId}")]
        public async Task<ActionResult> DeletePhotoAsync([FromRoute] int photoId)
        {
            await _service.DeleteUserPhotoAsync(photoId);
            return StatusCode(StatusCodes.Status204NoContent);
        }
    }
}
