using BusinessObject.Models.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObject.Models.Entity
{
    [Table("shipper_status")]
    public class ShipperStatus
    {
        [Key]
        [Column("shipper_id")]
        public Guid ShipperId { get; set; }

        [Required]
        [Column("status")]
        public ShipperStatusType Status { get; set; }

        [Required]
        [Column("last_updated")]
        public DateTime LastUpdated { get; set; } = DateTime.UtcNow;

        // Navigation properties
        [ForeignKey(nameof(ShipperId))]
        public virtual Manager? Manager { get; set; }
    }
}
