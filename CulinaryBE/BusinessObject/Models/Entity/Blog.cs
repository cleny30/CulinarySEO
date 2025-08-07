using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObject.Models.Entity
{
    [Table("blogs")]
    public class Blog
    {
        [Key]
        [Column("blog_id")]
        public Guid BlogId { get; set; }

        [Required]
        [Column("customer_id")]
        public Guid CustomerId { get; set; }

        [Required]
        [MaxLength(255)]
        [Column("title")]
        public string Title { get; set; } = string.Empty;

        [Required]
        [Column("content")]
        public string Content { get; set; } = string.Empty;

        [Required]
        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; }

        // Navigation properties
        [ForeignKey(nameof(CustomerId))]
        public virtual Customer? Customer { get; set; }
        public virtual ICollection<BlogCategoryMapping>? BlogCategoryMappings { get; set; }
        public virtual ICollection<BlogImage>? BlogImages { get; set; }
        public virtual ICollection<BlogSave>? BlogSaves { get; set; }
        public virtual ICollection<BlogComment>? BlogComments { get; set; }
    }
}
