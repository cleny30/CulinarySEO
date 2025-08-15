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
        [Column("manager_id")]
        public Guid ManagerId { get; set; }

        [Required]
        [MaxLength(255)]
        [Column("title")]
        public string Title { get; set; } = string.Empty;

        [Required]
        [Column("content")]
        public string Content { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        [Column("image_title")]
        public string ImageTitle { get; set; } = string.Empty;

        [Required]
        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; }

        // Navigation properties
        [ForeignKey(nameof(ManagerId))]
        public virtual Manager? Manager { get; set; }
        public virtual ICollection<BlogCategoryMapping>? BlogCategoryMappings { get; set; }
        public virtual ICollection<BlogSave>? BlogSaves { get; set; }
        public virtual ICollection<BlogComment>? BlogComments { get; set; }
    }
}
