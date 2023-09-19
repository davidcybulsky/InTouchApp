using InTouchApi.Application.Exceptions;
using InTouchApi.Application.Interfaces;
using InTouchApi.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace InTouchApi.Infrastructure.Hubs
{
    [Authorize]
    public sealed class ConnectionHub : Hub<IFriendPresence>
    {
        private readonly ApiContext _apiContext;
        private readonly IConnectionTracker _tracker;

        public ConnectionHub(ApiContext apiContext,
                             IConnectionTracker tracker)
        {
            _apiContext = apiContext;
            _tracker = tracker;
        }

        public async override Task OnConnectedAsync()
        {
            var user = await _apiContext.Users
                .Where(u => u.IsDeleted == false)
                .Include(u => u.Friends.Where(u => u.IsDeleted == false))
                .ThenInclude(f => f.Friend)
                .FirstOrDefaultAsync(u => u.Id == int.Parse(Context.User.FindFirstValue(ClaimTypes.NameIdentifier)))
                ?? throw new UnauthorizedException("Unauthorized access", $"Unauthorized user tried to connect to the connection hub");

            await _tracker.UserConnectedAsync(user.Id, Context.ConnectionId);

            await Groups.AddToGroupAsync(Context.ConnectionId, user.FriendGroupId);

            var friendGroups = user.Friends.Select(f => f.Friend.FriendGroupId);

            var connectedUsers = await _tracker.GetConnectedUsersAsync();

            foreach (var connectedUser in connectedUsers)
            {
                if (user.Friends.Any(f => f.FriendId == connectedUser))
                {
                    await Clients.Group(user.FriendGroupId).FriendIsOnline($"{connectedUser}");
                }
            }

            await Clients.Groups(friendGroups).FriendIsOnline(Context.User.FindFirstValue(ClaimTypes.NameIdentifier));
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var user = await _apiContext.Users
                .Where(u => u.IsDeleted == false)
                .Include(u => u.Friends.Where(u => u.IsDeleted == false))
                .ThenInclude(f => f.Friend)
                .FirstOrDefaultAsync(u => u.Id == int.Parse(Context.User.FindFirstValue(ClaimTypes.NameIdentifier)))
                ?? throw new UnauthorizedException("Unauthorized access", $"Unauthorized user tried to connect to the connection hub");

            await _tracker.UserDisconnectedAsync(user.Id, Context.ConnectionId);

            var groups = user.Friends.Select(f => f.Friend.FriendGroupId);

            if (!(await _tracker.IsConnected(user.Id)))
            {
                await Clients.Groups(groups).FriendIsOffline(Context.User.FindFirstValue(ClaimTypes.NameIdentifier));
            }

            await Groups.RemoveFromGroupAsync(Context.ConnectionId, user.FriendGroupId);

            await base.OnDisconnectedAsync(exception);
        }
    }
}
