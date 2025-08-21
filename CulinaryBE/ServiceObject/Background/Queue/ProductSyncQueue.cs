using BusinessObject.Models.Dto;

namespace ServiceObject.Background.Queue
{
    public interface IProductSyncQueue
    {
        void Enqueue(ProductSyncEvent syncEvent);
        bool TryDequeue(out ProductSyncEvent syncEvent);
    }

    public class ProductSyncQueue : IProductSyncQueue
    {
        private readonly Queue<ProductSyncEvent> _queue = new();
        private readonly object _lock = new();

        public void Enqueue(ProductSyncEvent syncEvent)
        {
            lock (_lock)
            {
                _queue.Enqueue(syncEvent);
            }
        }

        public bool TryDequeue(out ProductSyncEvent syncEvent)
        {
            lock (_lock)
            {
                if (_queue.Count > 0)
                {
                    syncEvent = _queue.Dequeue();
                    return true;
                }
            }

            syncEvent = default!;
            return false;
        }
    }

}
