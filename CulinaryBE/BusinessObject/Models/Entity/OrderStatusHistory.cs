using BusinessObject.Models.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObject.Models.Entity
{
    [Table("order_status_history")]
    public class OrderStatusHistory
    {
        [Key]
        [Column("history_id")]
        public Guid HistoryId { get; set; }

        [Required]
        [Column("order_id")]
        public Guid OrderId { get; set; }

        [Required]
        [Column("status")]
        public OrderStatus Status { get; set; }

        [Column("changed_by")]
        public Guid? ChangedBy { get; set; }

        [MaxLength(255)]
        [Column("change_note")]
        public string? ChangeNote { get; set; }

        [Column("changed_at")]
        public DateTime? ChangedAt { get; set; }

        // Navigation properties
        [ForeignKey(nameof(OrderId))]
        public virtual Order? Order { get; set; }

        [ForeignKey(nameof(ChangedBy))]
        public virtual Manager? ChangedByManager { get; set; }
    }
}
