namespace BusinessObject.Models.Dto
{
    public class GetProductDetailDto
    {
        public Guid ProductId { get; set; }

        public string ProductName { get; set; } = string.Empty;

        public string ShortDescription { get; set; } = string.Empty;
        public string LongDescription { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public decimal? Discount { get; set; }

        public decimal? FinalPrice { get; set; }
        public decimal AverageRating { get; set; }
        public string Slug { get; set; } = string.Empty;
        public string PageTitle { get; set; } = string.Empty;
        public string MetaDescription { get; set; } = string.Empty;

        public List<string> CategoryName { get; set; } = new List<string>();

        public List<string> ProductImages { get; set; } = new List<string>();
        public List<ProductReviewDto> Reviews { get; set; } = new List<ProductReviewDto>();
        public Dictionary<string, int> Stocks { get; set; } = new();
    }
}