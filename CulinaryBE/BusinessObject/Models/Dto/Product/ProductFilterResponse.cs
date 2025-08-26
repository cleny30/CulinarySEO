namespace BusinessObject.Models.Dto
{
    public class ProductFilterResponse
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string ShortDescription { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public string PageTitle { get; set; } = string.Empty;
        public string MetaDescription { get; set; } = string.Empty;

        public decimal Price { get; set; }
        public decimal FinalPrice { get; set; }
        public decimal? Discount { get; set; }

        /// <summary>
        /// Tổng tồn kho trên tất cả warehouse
        /// </summary>
        public int TotalQuantity { get; set; }

        public int TotalSold { get; set; }
        public int ReviewCount { get; set; }
        public decimal AverageRating { get; set; }

        public List<string> ProductImages { get; set; } = new();

        /// <summary>
        /// Danh mục (phục vụ filter/search)
        /// </summary>
        public List<int> CategoryIds { get; set; } = new();

        public Dictionary<string, int> Stocks { get; set; }

        /// <summary>
        /// Số lượng tồn kho tại warehouse hiện tại (nếu có filter WarehouseId)
        /// Nếu warehouse không có stock thì = 0
        /// </summary>
        public int CurrentWarehouseQuantity { get; set; }
    }

}
