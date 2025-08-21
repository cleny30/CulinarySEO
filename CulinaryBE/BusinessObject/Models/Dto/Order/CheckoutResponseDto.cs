namespace BusinessObject.Models.Dto
{
    public class CheckoutResponseDto
    {
        public Guid OrderId { get; set; }
        public string OrderStatus { get; set; } = string.Empty;
        public decimal Subtotal { get; set; }
        public decimal Discount { get; set; }
        public decimal ShippingFee { get; set; }
        public decimal TotalPrice { get; set; }

        public bool PaymentStatus { get; set; }
        public string PaymentMethod { get; set; } = string.Empty;

        // Tùy thích: echo allocation để debug
        public List<ItemAllocationDto> Allocations { get; set; } = new();
    }
}
