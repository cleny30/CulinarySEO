namespace BusinessObject.Models.Dto
{
    public class GetProductDto
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int TotalQuantity { get; set; }
        public decimal Price { get; set; }
        public decimal? Discount { get; set; }
        public decimal? FinalPrice { get; set; }
        public decimal? AverageRating { get; set; }
        public List<string> CategoryName { get; set; } = new List<string>();
        public List<string> ProductImages { get; set; } = new List<string>();
    }
}