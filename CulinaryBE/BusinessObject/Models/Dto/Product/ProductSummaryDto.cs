﻿namespace BusinessObject.Models.Dto.Product
{
    public class ProductSummaryDto
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }
    }
}
