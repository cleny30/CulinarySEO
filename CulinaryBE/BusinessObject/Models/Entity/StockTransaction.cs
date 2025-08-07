using BusinessObject.Models.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObject.Models.Entity
{
    [Table("stock_transactions")]
    public class StockTransaction
    {
        [Key]
        [Column("transaction_id")]
        public Guid TransactionId { get; set; }

        [Required]
        [Column("product_id")]
        public Guid ProductId { get; set; }

        [Required]
        [Column("warehouse_id")]
        public Guid WarehouseId { get; set; }

        [Required]
        [Column("quantity_change")]
        public int QuantityChange { get; set; }

        [Required]
        [Column("transaction_type")]
        public StockTransactionType TransactionType { get; set; }

        [Required]
        [MaxLength(255)]
        [Column("reason")]
        public string Reason { get; set; } = string.Empty;

        [Required]
        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Required]
        [Column("manager_id")]
        public Guid ManagerId { get; set; }

        // Navigation properties
        [ForeignKey(nameof(ProductId))]
        public virtual Product? Product { get; set; }

        [ForeignKey(nameof(WarehouseId))]
        public virtual Warehouse? Warehouse { get; set; }

        [ForeignKey(nameof(ManagerId))]
        public virtual Manager? Manager { get; set; }
    }
}

