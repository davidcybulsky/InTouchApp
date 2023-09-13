using InTouchApi.Application.Interfaces;
using InTouchApi.Application.Models;

namespace InTouchApi.Infrastructure.Services
{
    public class MessageService : IMessageService
    {
        public Task DeleteMessageAsync(int messageId)
        {
            throw new NotImplementedException();
        }

        public Task EditMessageAsync(UpdateMessageDto message)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<MessageDto>> GetMessageThreadAsync(int firstUserId, int secondUserId)
        {
            throw new NotImplementedException();
        }

        public Task SendMessageAsync(SendMessageDto message)
        {
            throw new NotImplementedException();
        }
    }
}
