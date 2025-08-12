using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObject.Models.Entity
{
    [Table("product_history")]
    public class ProductHistory
    {
        [Key]
        [Column("history_id")]
        public Guid HistoryId { get; set; }

        [Required]
        [Column("product_id")]
        public Guid ProductId { get; set; }

        [Required]
        [MaxLength(100)]
        [Column("product_name")]
        public string ProductName { get; set; } = string.Empty;

        [Required]
        [Column("description")]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Column("price", TypeName = "decimal(10,2)")]
        public decimal? Price { get; set; }

        [Column("discount", TypeName = "decimal(5,2)")]
        public decimal? Discount { get; set; }

        [Required]
        [Column("changed_by")]
        public Guid ChangedBy { get; set; }

        [Required]
        [MaxLength(255)]
        [Column("change_reason")]
        public string ChangeReason { get; set; } = string.Empty;

        [Required]
        [Column("changed_at")]
        public DateTime ChangedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        [ForeignKey(nameof(ProductId))]
        public virtual Product? Product { get; set; }

        [ForeignKey(nameof(ChangedBy))]
        public virtual Manager? ChangedByManager { get; set; }
    }
}
