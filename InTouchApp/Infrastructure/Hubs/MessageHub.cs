using InTouchApi.Application.Exceptions;
using InTouchApi.Application.Interfaces;
using InTouchApi.Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace InTouchApi.Infrastructure.Hubs
{
    [Authorize]
    public sealed class MessageHub : Hub<IChatClient>
    {
        private readonly IMessageService _service;

        public MessageHub(IMessageService service)
        {
            _service = service;
        }

        public override async Task OnConnectedAsync()
        {
            var otherUser = int.Parse(Context.GetHttpContext().Request.Query["user"]);
            var user = int.Parse(Context.User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (user == otherUser)
            {
                throw new BadRequestException("You can not send a message to yourself",
                    $"User with id: {user} tried to send a message to itself");
            }

            var group = await _service.GetMessageGroupName(user, otherUser);

            await Groups.AddToGroupAsync(Context.ConnectionId, group);

            var messages = await _service.GetMessageThreadAsync(user, otherUser);

            await Clients.Group(group).GetMessageThread(messages);
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var otherUser = int.Parse(Context.GetHttpContext().Request.Query["user"]);
            var user = int.Parse(Context.User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (user == otherUser)
            {
                throw new BadRequestException("You can not send a message to yourself",
                    $"User with id: {user} tried to send a message to itself");
            }

            var group = await _service.GetMessageGroupName(user, otherUser);

            await Groups.RemoveFromGroupAsync(Context.ConnectionId, group);

            await base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessageAsync(SendMessageDto sendMessageDto)
        {
            var user = int.Parse(Context.User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (!(await _service.DoesRecipientExist(int.Parse(sendMessageDto.RecipientId))))
            {
                throw new BadRequestException("You can not send a message to the user",
                                    $"User with id: {user} tried to send a message to user that does not exist");
            }

            if (user == int.Parse(sendMessageDto.RecipientId))
            {
                throw new BadRequestException("You can not send a message to yourself",
                    $"User with id: {user} tried to send a message to itself");
            }

            var message = await _service.SendMessageAsync(user, sendMessageDto);
            var group = await _service.GetMessageGroupName(user, int.Parse(sendMessageDto.RecipientId));

            if (group is null || message is null)
            {
                throw new HubException();
            }

            await Clients.Group(group).NewMessage(message);
        }
    }
}
