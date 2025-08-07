using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObject.Models.Entity
{
    [Table("warehouses")]
    public class Warehouse
    {
        [Key]
        [Column("warehouse_id")]
        public Guid WarehouseId { get; set; }

        [Required]
        [MaxLength(100)]
        [Column("warehouse_name")]
        public string WarehouseName { get; set; } = string.Empty;

        [Required]
        [Column("location")]
        public string Location { get; set; } = string.Empty;

        [Required]
        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public virtual ICollection<Stock>? Stocks { get; set; }
        public virtual ICollection<StockTransaction>? StockTransactions { get; set; }
    }
}
