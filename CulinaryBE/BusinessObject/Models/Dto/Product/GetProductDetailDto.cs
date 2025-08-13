using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace BusinessObject.Models.Dto
{
    public class GetProductDetailDto
    {
        public Guid ProductId { get; set; }

        public string ProductName { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public decimal? Discount { get; set; }

        public decimal? FinalPrice { get; set; }

        public string CategoryName { get; set; } = string.Empty;

        public List<string> ProductImages { get; set; } = new List<string>();
        public List<ProductReviewDto> Reviews { get; set; } = new List<ProductReviewDto>();
    }
}