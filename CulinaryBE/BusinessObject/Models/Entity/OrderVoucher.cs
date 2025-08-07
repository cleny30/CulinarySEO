using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObject.Models.Entity
{
    [Table("order_vouchers")]
    public class OrderVoucher
    {
        [Key]
        [Column("order_voucher_id")]
        public Guid OrderVoucherId { get; set; }

        [Required]
        [Column("order_id")]
        public Guid OrderId { get; set; }

        [Required]
        [Column("voucher_id")]
        public Guid VoucherId { get; set; }

        [Required]
        [Column("applied_discount", TypeName = "decimal(10,2)")]
        public decimal AppliedDiscount { get; set; }

        [Required]
        [Column("applied_at")]
        public DateTime AppliedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        [ForeignKey(nameof(OrderId))]
        public virtual Order? Order { get; set; }

        [ForeignKey(nameof(VoucherId))]
        public virtual Voucher? Voucher { get; set; }
    }
}
