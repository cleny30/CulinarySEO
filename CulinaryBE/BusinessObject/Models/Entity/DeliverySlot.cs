using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObject.Models.Entity
{
    [Table("delivery_slots")]
    public class DeliverySlot
    {
        [Key]
        [Column("slot_id")]
        public Guid SlotId { get; set; }

        [Required]
        [Column("order_id")]
        public Guid OrderId { get; set; }

        [Required]
        [Column("start_time")]
        public DateTime StartTime { get; set; }

        [Required]
        [Column("end_time")]
        public DateTime EndTime { get; set; }

        [ForeignKey(nameof(OrderId))]
        public virtual Order? Order { get; set; }
    }
}
