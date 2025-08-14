namespace BusinessObject.Models.Dto
{
    public class CartDto
    {
        public Guid CartId { get; set; }
        public decimal SubTotal { get; set; }
        public List<CartItemDto>? CartItems { get; set; } 
    }

    public class CartItemDto
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }= string.Empty;
        public decimal Price { get; set; }
        public decimal FinalPrice { get; set; }
        public int Quantity { get; set; }
        public string ProductImage { get; set; } = string.Empty;
    }
}
