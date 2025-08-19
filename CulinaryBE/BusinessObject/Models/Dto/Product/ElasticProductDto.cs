namespace BusinessObject.Models.Dto
{
    public class ElasticProductDto
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
        public List<int> CategoryIds { get; set; } = new List<int>();
        public DateTime CreatedAt { get; set; }
    }
}
