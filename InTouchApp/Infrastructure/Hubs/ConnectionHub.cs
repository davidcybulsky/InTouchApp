using Microsoft.AspNetCore.SignalR;

namespace InTouchApi.Infrastructure.Hubs
{
    public sealed class ConnectionHub : Hub
    {
        public async override Task OnConnectedAsync()
        {
            await Clients.All.SendAsync("ReceiveMessage", $"Context.ConnectionId has joined");
        }
    }
}
