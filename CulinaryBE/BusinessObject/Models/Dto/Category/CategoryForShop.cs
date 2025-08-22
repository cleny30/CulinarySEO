namespace BusinessObject.Models.Dto
{
    public class CategoryForShop
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public string PageTitle { get; set; } = string.Empty;
        public string MetaDescription { get; set; } = string.Empty;
        public int ProductCount { get; set; }
    }
}
