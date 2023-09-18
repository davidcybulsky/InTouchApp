namespace InTouchApi.Application.Interfaces
{
    public interface IChatClient
    {
        Task SendMessage(string message);
    }
}
