using InTouchApi.Application.Interfaces;

namespace InTouchApi.Infrastructure.Hubs
{
    public sealed class ConnectionTracker : IConnectionTracker
    {
        private static readonly Dictionary<int, List<string>> _connectedUsers = new();

        public Task<IEnumerable<int>> GetConnectedUsersAsync()
        {
            return Task.FromResult(_connectedUsers.Keys.ToList() as IEnumerable<int>);
        }

        public Task<bool> IsConnected(int identity)
        {
            return Task.FromResult(_connectedUsers.ContainsKey(identity));
        }

        public Task UserConnectedAsync(int identity, string connectionId)
        {
            lock (_connectedUsers)
            {
                if (_connectedUsers.ContainsKey(identity))
                {
                    _connectedUsers[identity].Add(connectionId);
                }
                else
                {
                    _connectedUsers.Add(identity, new List<string> { connectionId });
                }
            }
            return Task.CompletedTask;
        }

        public Task UserDisconnectedAsync(int identity, string connectionId)
        {
            lock (_connectedUsers)
            {
                if (!_connectedUsers.ContainsKey(identity)) return Task.CompletedTask;

                _connectedUsers[identity].Remove(connectionId);

                if (_connectedUsers[identity].Count == 0)
                {
                    _connectedUsers.Remove(identity);
                }
            }
            return Task.CompletedTask;
        }
    }
}
