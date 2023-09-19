using InTouchApi.Application.Models;

namespace InTouchApi.Application.Interfaces
{
    public interface IChatClient
    {
        Task NewMessage(MessageDto message);
        Task GetMessageThread(IEnumerable<MessageDto> thread);
    }
}
