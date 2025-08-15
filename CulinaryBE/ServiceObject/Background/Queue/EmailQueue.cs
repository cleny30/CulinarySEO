using BusinessObject.Models.Dto;

namespace ServiceObject.Background.Queue
{
    public interface IEmailQueue
    {
        void Enqueue(EmailQueueItem emailItem);
        bool TryDequeue(out EmailQueueItem emailItem);
    }
    public class EmailQueue : IEmailQueue
    {
        private readonly Queue<EmailQueueItem> _queue = new();
        private readonly object _lock = new();

        public void Enqueue(EmailQueueItem emailItem)
        {
            lock (_lock)
            {
                _queue.Enqueue(emailItem);
            }
        }

        public bool TryDequeue(out EmailQueueItem emailItem)
        {
            lock (_lock)
            {
                if (_queue.Count > 0)
                {
                    emailItem = _queue.Dequeue();
                    return true;
                }
            }
            emailItem = default!;
            return false;
        }
    }
}
