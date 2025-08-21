using BusinessObject.Models.Enum;
using System.ComponentModel.DataAnnotations;

namespace BusinessObject.Models.Dto
{
    public class ProductFilterRequest
    {
        [Required]
        public Guid WarehouseId { get; set; }
        public List<int>? CategoryIds { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public bool? IsAvailable { get; set; }
        public ProductSortOption SortBy { get; set; } = ProductSortOption.NameAZ;
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
}
