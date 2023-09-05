using InTouchApi.Application.Interfaces;
using InTouchApi.Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InTouchApi.Presentation.Controllers
{
    [ApiController]
    [Route("friend")]
    public class FriendController : ControllerBase
    {
        private readonly IFriendService _service;

        public FriendController(IFriendService service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<FriendDto>>> GetFriendsAsync()
        {
            var friendDtos = await _service.GetFriendsAsync();
            return StatusCode(StatusCodes.Status200OK, friendDtos);
        }

        [HttpGet("requests")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<FriendDto>>> GetFriendRequestsAsync()
        {
            var friendRequestDtos = await _service.GetFriendRequestsAsync();
            return StatusCode(StatusCodes.Status200OK, friendRequestDtos);
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<FriendDto>>> GetFriendsAsync([FromRoute] int userId)
        {
            var friendDtos = await _service.GetUserFriendsAsync(userId);
            return StatusCode(StatusCodes.Status200OK, friendDtos);
        }

        [HttpGet("user/{userId}/requests")]
        public async Task<ActionResult<IEnumerable<FriendDto>>> GetFriendRequestsAsync([FromRoute] int userId)
        {
            var friendRequestDtos = await _service.GetUserFriendRequestsAsync(userId);
            return StatusCode(StatusCodes.Status200OK, friendRequestDtos);
        }

        [HttpPut("accept/{userId}")]
        [Authorize]
        public async Task<ActionResult> AcceptFriendRequestAsync([FromRoute] int userId)
        {
            await _service.AcceptFriendRequestAsync(userId);
            return StatusCode(StatusCodes.Status202Accepted);
        }

        [HttpPost("send/{userId}")]
        [Authorize]
        public async Task<ActionResult> SendFriendRequestAsync([FromRoute] int userId)
        {
            await _service.SendFriendRequestAsync(userId);
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpDelete("{friendId}")]
        [Authorize]
        public async Task<ActionResult> DeleteFriendAsync([FromRoute] int friendId)
        {
            await _service.DeleteFriendAsync(friendId);
            return StatusCode(StatusCodes.Status204NoContent);
        }
    }
}
