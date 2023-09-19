using InTouchApi.Domain.Entities;

namespace InTouchApi.Application.Interfaces
{
    public interface IMessageRepository
    {
        Task<Message> GetMessageAsync(int messageId);
        Task<IEnumerable<Message>> GetMessageThreadAsync(int firstUserId, int secondUserId);
        Task<Message> SendMessageAsync(Message message);
        Task<Message> EditMessageContentAsync(Message message);
        Task DeleteMessageAsync(int messageId);
        Task<bool> DoesRecipientExist(int recipientId);
    }
}
