using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObject.Models.Entity
{
    public class NotificationManager
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("notification_id")]
        public int NotificationId { get; set; }

        [Required]
        [Column("manager_id")]
        public Guid ManagerId { get; set; }

        [Required]
        [StringLength(125)]
        [Column("message")]
        public string Message { get; set; } = null!;

        [Required]
        [Column("create_date")]
        public DateTime CreateDate { get; set; } = DateTime.UtcNow;

        [Required]
        [Column("is_read")]
        public bool IsRead { get; set; } = false;

        [ForeignKey(nameof(ManagerId))]
        public virtual Manager? Manager { get; set; }
    }
}
