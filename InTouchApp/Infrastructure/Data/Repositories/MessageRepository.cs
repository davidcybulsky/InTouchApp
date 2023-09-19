using InTouchApi.Application.Interfaces;
using InTouchApi.Domain.Entities;

namespace InTouchApi.Infrastructure.Data.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        public Task DeleteMessageAsync(int messageId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DoesRecipientExist(int recipientId)
        {
            throw new NotImplementedException();
        }

        public Task EditMessageAsync(Message message)
        {
            throw new NotImplementedException();
        }

        public Task<Message> EditMessageContentAsync(Message message)
        {
            throw new NotImplementedException();
        }

        public Task<Message> GetMessageAsync(int messageId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Message>> GetMessageThreadAsync(int firstUserId, int secondUserId)
        {
            throw new NotImplementedException();
        }

        public Task SendMessageAsync(Message message)
        {
            throw new NotImplementedException();
        }

        Task<Message> IMessageRepository.SendMessageAsync(Message message)
        {
            throw new NotImplementedException();
        }
    }
}