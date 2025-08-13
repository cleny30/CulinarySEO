using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessObject.Models.Dto
{
    public class ProductReviewDto
    {
        public Guid ReviewId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public int? Rating { get; set; }
        public string Comment { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    }
}