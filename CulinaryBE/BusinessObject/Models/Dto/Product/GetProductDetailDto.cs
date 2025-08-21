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

        public List<string> CategoryName { get; set; } = new List<string>();

        public List<string> ProductImages { get; set; } = new List<string>();
        public List<ProductReviewDto> Reviews { get; set; } = new List<ProductReviewDto>();
    }
}