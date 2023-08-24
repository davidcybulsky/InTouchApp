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

        [HttpGet("user/{id}")]
        public async Task<ActionResult<IEnumerable<FriendDto>>> GetFriendsAsync([FromRoute] int id)
        {
            var friendDtos = await _service.GetUserFriendsAsync(id);
            return StatusCode(StatusCodes.Status200OK, friendDtos);
        }

        [HttpGet("user/{id}/requests")]
        public async Task<ActionResult<IEnumerable<FriendDto>>> GetFriendRequestsAsync([FromRoute] int id)
        {
            var friendRequestDtos = await _service.GetUserFriendRequestsAsync(id);
            return StatusCode(StatusCodes.Status200OK, friendRequestDtos);
        }

        [HttpPut("accept/{id}")]
        [Authorize]
        public async Task<ActionResult> AcceptFriendRequestAsync([FromRoute] int id)
        {
            await _service.AcceptFriendRequestAsync(id);
            return StatusCode(StatusCodes.Status202Accepted);
        }

        [HttpPost("send/{id}")]
        [Authorize]
        public async Task<ActionResult> SendFriendRequestAsync([FromRoute] int id)
        {
            await _service.SendFriendRequestAsync(id);
            return StatusCode(StatusCodes.Status201Created);
        }
    }
}
