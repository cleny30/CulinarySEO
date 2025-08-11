using System.Collections.Concurrent;

namespace ServiceObject.Background
{
    public interface ITokenSaveQueue
    {
        void EnqueueToken(Guid userId, string refreshToken, DateTime expiry);
        bool TryDequeue(out (Guid UserId, string RefreshToken, DateTime Expiry) tokenData);
    }
    public class TokenSaveQueue: ITokenSaveQueue
    {
        private readonly ConcurrentQueue<(Guid, string, DateTime)> _queue = new();

        public void EnqueueToken(Guid userId, string refreshToken, DateTime expiry)
        {
            _queue.Enqueue((userId, refreshToken, expiry));
        }

        public bool TryDequeue(out (Guid UserId, string RefreshToken, DateTime Expiry) tokenData)
        {
            return _queue.TryDequeue(out tokenData);
        }
    }
}
