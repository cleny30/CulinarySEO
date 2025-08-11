using BusinessObject.Models.Dto;
using BusinessObject.Models.Enum;
using System.Collections.Concurrent;

namespace ServiceObject.Background
{
    public interface ITokenSaveQueue
    {
        void EnqueueToken(Guid userId, string refreshToken, DateTime expiry, AccountType accountType);
        bool TryDequeue(out TokenData tokenData);
    }
    public class TokenSaveQueue: ITokenSaveQueue
    {
        private readonly ConcurrentQueue<TokenData> _queue = new();

        public void EnqueueToken(Guid userId, string refreshToken, DateTime expiry, AccountType accountType)
        {
            _queue.Enqueue(new TokenData(userId, refreshToken, expiry, accountType));
        }

        public bool TryDequeue(out TokenData tokenData)
        {
            return _queue.TryDequeue(out tokenData);
        }
    }
}
