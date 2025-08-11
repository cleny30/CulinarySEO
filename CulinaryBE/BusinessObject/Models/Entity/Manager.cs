using BusinessObject.Models.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reactive;

namespace BusinessObject.Models.Entity
{
    [Table("managers")]
    public class Manager
    {
        [Key]
        [Column("manager_id")]
        public Guid ManagerId { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("username")]
        public string Username { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        [Column("password")]
        public string Password { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        [Column("email")]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        [Column("full_name")]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [MaxLength(20)]
        [Column("phone")]
        public string Phone { get; set; } = string.Empty;

        [Required]
        [Column("role_id")]
        public int RoleId { get; set; }

        [Required]
        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; }

        [MaxLength(255)]
        [Column("token")]
        public string? Token { get; set; }

        [Column("expires_at")]
        public DateTime? ExpiresAt { get; set; }

        [Column("revoked")]
        public bool? Revoked { get; set; }

        [Required]
        [Column("status")]
        public UserStatus Status { get; set; }

        // Navigation properties
        [ForeignKey(nameof(RoleId))]
        public virtual Role? Role { get; set; }
        public virtual ShipperStatus? ShipperStatus { get; set; }
        public virtual ICollection<ProductHistory>? ProductHistories { get; set; }
        public virtual ICollection<StockTransaction>? StockTransactions { get; set; }
        public virtual ICollection<Order>? ShippedOrders { get; set; }
        public virtual ICollection<OrderStatusHistory>? OrderStatusHistories { get; set; }
        public virtual ICollection<NotificationManager>? NotificationManagers { get; set; }
        public virtual ICollection<ChatSession>? ChatSessions { get; set; }
    }
}
