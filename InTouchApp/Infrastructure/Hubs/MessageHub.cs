using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace InTouchApi.Infrastructure.Hubs
{
    [Authorize]
    public sealed class MessageHub : Hub
    {
    }
}
