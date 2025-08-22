using System.ComponentModel.DataAnnotations;

namespace BusinessObject.Models.Dto
{
    public class ElasticProductDto
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string ShortDescription { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public decimal FinalPrice { get; set; }
        public decimal? Discount { get; set; }
        public int TotalSold { get; set; }
        public int ReviewCount { get; set; }
        public decimal AverageRating { get; set; }
        public string Slug { get; set; } = string.Empty;
        public string PageTitle { get; set; } = string.Empty;
        public string MetaDescription { get; set; } = string.Empty;
        public List<string> ProductImages { get; set; } = new();
        public List<int> CategoryIds { get; set; } = new();
        public DateTime CreatedAt { get; set; }
        public Dictionary<string, int> Stocks { get; set; } = new();

    }
}
