namespace BusinessObject.Models.Dto
{
    public class CategoryForShop
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public int ProductCount { get; set; }
    }
}
