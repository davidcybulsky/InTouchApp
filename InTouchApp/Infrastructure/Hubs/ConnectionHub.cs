using InTouchApi.Application.Exceptions;
using InTouchApi.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace InTouchApi.Infrastructure.Hubs
{
    [Authorize]
    public sealed class ConnectionHub : Hub
    {
        private readonly ApiContext _apiContext;

        public ConnectionHub(ApiContext apiContext)
        {
            _apiContext = apiContext;
        }

        public async override Task OnConnectedAsync()
        {
            var user = await _apiContext.Users
                .Where(u => u.IsDeleted == false)
                .Include(u => u.Friends.Where(u => u.IsDeleted == false))
                .ThenInclude(f => f.Friend)
                .FirstOrDefaultAsync(u => u.Id == int.Parse(Context.User.FindFirstValue(ClaimTypes.NameIdentifier)))
                ?? throw new UnauthorizedException("Unauthorized access", $"Unauthorized user tried to connect to the connection hub");

            await Groups.AddToGroupAsync(Context.ConnectionId, user.FriendGroupId);

            var groups = user.Friends.Select(f => f.Friend.FriendGroupId);

            await Clients.Groups(groups).SendAsync("FriendIsOnline", $"{Context.User.FindFirstValue(ClaimTypes.Name)} has joined");
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var user = await _apiContext.Users
                .Where(u => u.IsDeleted == false)
                .Include(u => u.Friends.Where(u => u.IsDeleted == false))
                .ThenInclude(f => f.Friend.IsDeleted == false)
                .FirstOrDefaultAsync(u => u.Id == int.Parse(Context.User.FindFirstValue(ClaimTypes.NameIdentifier)))
                ?? throw new UnauthorizedException("Unauthorized access", $"Unauthorized user tried to connect to the connection hub");

            await Groups.RemoveFromGroupAsync(Context.ConnectionId, user.FriendGroupId);

            var groups = user.Friends.Select(f => f.Friend.FriendGroupId);

            await Clients.Groups(groups).SendAsync("FriendIsOffline", $"{Context.User.FindFirstValue(ClaimTypes.Name)} has disconnected");
            await base.OnDisconnectedAsync(exception);
        }
    }
}
