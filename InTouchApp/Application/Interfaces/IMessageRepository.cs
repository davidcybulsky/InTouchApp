using InTouchApi.Domain.Entities;

namespace InTouchApi.Application.Interfaces
{
    public interface IMessageRepository
    {
        Task<Message> GetMessageAsync(int messageId);
        Task<IEnumerable<Message>> GetMessageThreadAsync(int firstUserId, int secondUserId);
        Task SendMessageAsync(Message message);
        Task EditMessageAsync(Message message);
        Task DeleteMessageAsync(int messageId);
    }
}
