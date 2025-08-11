using BusinessObject.Models.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObject.Models.Entity
{
    [Table("chat_sessions")]
    public class ChatSession
    {
        [Key]
        [Column("chat_session_id")]
        public Guid ChatSessionId { get; set; }

        [Required]
        [Column("customer_id")]
        public Guid CustomerId { get; set; }

        [Required]
        [Column("manager_id")]
        public Guid? ManagerId { get; set; }

        [Required]
        [Column("support_type")]
        public SupportTypeEnum SupportType { get; set; }

        [Required]
        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [ForeignKey(nameof(CustomerId))]
        public virtual Customer? Customer { get; set; }

        [ForeignKey(nameof(ManagerId))]
        public virtual Manager? Manager { get; set; }

        public virtual ICollection<ChatHistory>? ChatHistories { get; set; }

    }
}
