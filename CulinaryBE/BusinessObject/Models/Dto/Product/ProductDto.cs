namespace BusinessObject.Models.Dto
{
    public class ProductDto
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public decimal FinalPrice { get; set; }
        public decimal? Discount { get; set; }
        public int TotalQuantity { get; set; }
        public int TotalSold { get; set; }
        public int ReviewCount { get; set; }
        public decimal AverageRating { get; set; }
        public List<string> ProductImages { get; set; } = new List<string>();
    }
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
