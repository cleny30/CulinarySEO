using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObject.Models.Entity
{
    [Table("blog_saves")]
    public class BlogSave
    {
        [Key]
        [Column("save_id")]
        public Guid SaveId { get; set; }

        [Required]
        [Column("blog_id")]
        public Guid BlogId { get; set; }

        [Required]
        [Column("customer_id")]
        public Guid CustomerId { get; set; }

        [Required]
        [Column("saved_at")]
        public DateTime SavedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        [ForeignKey(nameof(BlogId))]
        public virtual Blog? Blog { get; set; }

        [ForeignKey(nameof(CustomerId))]
        public virtual Customer? Customer { get; set; }
    }
}
