using InTouchApi.Application.Models;

namespace InTouchApi.Application.Interfaces
{
    public interface IMessageService
    {
        Task<IEnumerable<MessageDto>> GetMessageThreadAsync(int firstUserId, int secondUserId);
        Task<MessageDto> SendMessageAsync(int identity, SendMessageDto message);
        Task<string> GetMessageGroupName(int user, int otherUser);
        Task<bool> DoesRecipientExist(int recipientId);
    }
}
