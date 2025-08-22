namespace BusinessObject.Models.Dto
{
    public record ProductSyncEvent
    {
        public Guid? ProductId { get; init; }
        public string Action { get; init; } = string.Empty;
        public DateTime Timestamp { get; init; } = DateTime.UtcNow;

        public ProductSyncEvent() { }

        public ProductSyncEvent(Guid? productId, string action, DateTime timestamp)
        {
            ProductId = productId;
            Action = action;
            Timestamp = timestamp;
        }
    }
}
