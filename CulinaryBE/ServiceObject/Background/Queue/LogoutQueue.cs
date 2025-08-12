using BusinessObject.Models.Dto;
using BusinessObject.Models.Enum;

namespace ServiceObject.Background.Queue
{
    public interface ILogoutQueue
    {
        void Enqueue(string refreshToken, AccountType accountType);
        bool TryDequeue(out LogoutData logoutData);
    }
    public class LogoutQueue : ILogoutQueue
    {
        private readonly Queue<LogoutData> _queue = new();
        private readonly object _lock = new();

        public void Enqueue(string refreshToken, AccountType accountType)
        {
            lock (_lock)
            {
                _queue.Enqueue(new LogoutData(refreshToken, accountType));
            }
        }

        public bool TryDequeue(out LogoutData logoutData)
        {
            lock (_lock)
            {
                if (_queue.Count > 0)
                {
                    logoutData = _queue.Dequeue();
                    return true;
                }
            }
            logoutData = default;
            return false;
        }
    }
}
