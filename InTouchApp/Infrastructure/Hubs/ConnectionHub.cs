using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace InTouchApi.Infrastructure.Hubs
{
    [Authorize]
    public sealed class ConnectionHub : Hub
    {
        public async override Task OnConnectedAsync()
        {
            await Clients.All.SendAsync("UserIsOnline", $"{Context.User.Identity.Name} has joined");
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await Clients.All.SendAsync("UserIsOffline", $"{Context.User.Identity.Name} has disconected");
            await base.OnDisconnectedAsync(exception);
        }
    }
}
