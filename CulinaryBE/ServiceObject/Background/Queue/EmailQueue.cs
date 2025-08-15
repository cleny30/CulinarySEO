using BusinessObject.Models.Dto;

namespace ServiceObject.Background.Queue
{
    public interface IEmailQueue
    {
        void Enqueue(string toEmail, string otp);
        bool TryDequeue(out EmailQueueItem emailItem);
    }
    public class EmailQueue
    {
        private readonly Queue<EmailQueueItem> _queue = new();
        private readonly object _lock = new();

        public void Enqueue(string toEmail, string otp)
        {
            lock (_lock)
            {
                _queue.Enqueue(new EmailQueueItem(toEmail, otp));
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
