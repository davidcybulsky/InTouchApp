using InTouchApi.Application.Models;

namespace InTouchApi.Application.Interfaces
{
    public interface IMessageService
    {
        Task<IEnumerable<MessageDto>> GetMessageThreadAsync(int firstUserId, int secondUserId);
        Task SendMessageAsync(SendMessageDto message);
        Task EditMessageAsync(UpdateMessageDto message);
        Task DeleteMessageAsync(int messageId);
    }
}
