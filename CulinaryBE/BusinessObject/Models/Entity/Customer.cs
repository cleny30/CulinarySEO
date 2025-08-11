using BusinessObject.Models.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObject.Models.Entity
{
    [Table("customers")]
    public class Customer
    {
        [Key]
        [Column("customer_id")]
        public Guid CustomerId { get; set; }

        [Required]
        [MaxLength(100)]
        [Column("full_name")]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        [Column("email")]
        public string Email { get; set; } = string.Empty;

        [MaxLength(255)]
        [Column("password")]
        public string? Password { get; set; }

        [Required]
        [MaxLength(20)]
        [Column("phone")]
        public string Phone { get; set; } = string.Empty;

        [MaxLength(255)]
        [Column("profile_pic")]
        public string? ProfilePic { get; set; }

        [Required]
        [Column("status")]
        public UserStatus Status { get; set; } = UserStatus.Active;

        [Required]
        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [MaxLength(255)]
        [Column("token")]
        public string? Token { get; set; }

        [Column("expires_at")]
        public DateTime? ExpiresAt { get; set; }

        [Column("revoked")]
        public bool? Revoked { get; set; }

        // Navigation properties
        public virtual ICollection<CustomerAddress>? CustomerAddresses { get; set; }
        public virtual ICollection<Cart>? Carts { get; set; }
        public virtual ICollection<Order>? Orders { get; set; }
        public virtual ICollection<ProductReview>? ProductReviews { get; set; }
        public virtual ICollection<Blog>? Blogs { get; set; }
        public virtual ICollection<BlogSave>? BlogSaves { get; set; }
        public virtual ICollection<BlogComment>? BlogComments { get; set; }
        public virtual ICollection<NotificationCustomer>? NotificationCustomers { get; set; }
        public virtual ICollection<ChatSession>? ChatSessions { get; set; }
    }
}
