using BusinessObject.Models.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObject.Models.Entity
{
    [Table("orders")]
    public class Order
    {
        [Key]
        [Column("order_id")]
        public Guid OrderId { get; set; }

        [Required]
        [Column("customer_id")]
        public Guid CustomerId { get; set; }

        [Required]
        [Column("order_status")]
        public OrderStatus OrderStatus { get; set; }

        [Required]
        [Column("shipping_fee", TypeName = "decimal(10,2)")]
        public decimal ShippingFee { get; set; }

        // TotalPrice = subtotal of all order details + shipping fee
        [Required]
        [Column("total_price", TypeName = "decimal(10,2)")]
        public decimal TotalPrice { get; set; }

        [Required]
        [Column("shipping_address")]
        public string ShippingAddress { get; set; } = string.Empty;

        [Column("shipper_id")]
        public Guid? ShipperId { get; set; }

        [Required]
        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; } 

        // Navigation properties
        [ForeignKey(nameof(CustomerId))]
        public virtual Customer? Customer { get; set; }

        [ForeignKey(nameof(ShipperId))]
        public virtual Manager? Shipper { get; set; }

        public virtual ICollection<OrderVoucher>? OrderVouchers { get; set; }
        public virtual ICollection<OrderStatusHistory>? OrderStatusHistories { get; set; }
        public virtual ICollection<OrderDetail>? OrderDetails { get; set; }
        public virtual ICollection<DeliverySlot>? DeliverySlots { get; set; }

    }
}
