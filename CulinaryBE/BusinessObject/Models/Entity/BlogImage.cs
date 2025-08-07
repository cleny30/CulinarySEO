using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObject.Models.Entity
{
    [Table("blog_images")]
    public class BlogImage
    {
        [Key]
        [Column("image_id")]
        public Guid ImageId { get; set; }

        [Required]
        [Column("blog_id")]
        public Guid BlogId { get; set; }

        [Required]
        [MaxLength(255)]
        [Column("image_url")]
        public string ImageUrl { get; set; } = string.Empty;

        [Required]
        [Column("is_primary")]
        public bool IsPrimary { get; set; } = false;

        [Required]
        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        [ForeignKey(nameof(BlogId))]
        public virtual Blog? Blog { get; set; }
    }
}
