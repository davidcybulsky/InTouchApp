namespace InTouchApi.Application.Interfaces
{
    public interface IFriendPresence
    {
        Task FriendIsOnline(string message);
        Task FriendIsOffline(string message);
    }
}
