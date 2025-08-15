namespace BusinessObject.Models.Dto
{
    public class ProductFilterRequest
    {
        public List<int>? CategoryIds { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public bool? IsAvailable { get; set; }
        public string? SortBy { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
}
