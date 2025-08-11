using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObject.Models.Entity
{
    [Table("chat_histories")]
    public class ChatHistory
    {
        [Key]
        [Column("chat_id")]
        public Guid ChatId { get; set; }

        public Guid ChatSessionId { get; set; }

        [Column("message")]
        public string Message { get; set; } = string.Empty;

        // Navigation properties
        [ForeignKey(nameof(ChatSessionId))]
        public virtual ChatSession? ChatSession { get; set; }
    }
}
