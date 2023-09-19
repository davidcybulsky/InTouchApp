namespace InTouchApi.Application.Interfaces
{
    public interface IConnectionTracker
    {
        Task UserConnectedAsync(int identity, string connectionId);
        Task UserDisconnectedAsync(int identity, string connectionId);
        Task<IEnumerable<int>> GetConnectedUsersAsync();
        Task<bool> IsConnected(int identity);
    }
}
