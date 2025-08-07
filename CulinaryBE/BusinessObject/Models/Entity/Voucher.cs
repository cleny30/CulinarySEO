using BusinessObject.Models.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObject.Models.Entity
{
    [Table("vouchers")]
    public class Voucher
    {
        [Key]
        [Column("voucher_id")]
        public Guid VoucherId { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("voucher_code")]
        public string VoucherCode { get; set; } = string.Empty;

        [Column("description")]
        public string? Description { get; set; }

        [Required]
        [Column("discount_type")]
        public VoucherDiscountType DiscountType { get; set; }

        [Required]
        [Column("discount_value", TypeName = "decimal(10,2)")]
        public decimal DiscountValue { get; set; }

        [Required]
        [Column("min_order_value", TypeName = "decimal(10,2)")]
        public decimal MinOrderValue { get; set; } = 0.00m;

        [Column("max_discount_value", TypeName = "decimal(10,2)")]
        public decimal? MaxDiscountValue { get; set; }

        [Column("max_vouchers_per_order")]
        public int MaxVouchersPerOrder { get; set; } = 1;

        [Required]
        [Column("start_date")]
        public DateTime StartDate { get; set; }

        [Required]
        [Column("end_date")]
        public DateTime EndDate { get; set; }

        [Required]
        [Column("usage_limit")]
        public int UsageLimit { get; set; }

        [Column("used_count")]
        public int? UsedCount { get; set; } = 0;

        [Required]
        [Column("status")]
        public VoucherStatus Status { get; set; } = VoucherStatus.Active;

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public virtual ICollection<OrderVoucher>? OrderVouchers { get; set; }
    }
}
