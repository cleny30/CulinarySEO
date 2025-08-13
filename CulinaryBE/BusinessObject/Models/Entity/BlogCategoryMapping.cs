using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObject.Models.Entity
{
    [Table("blog_category_mappings")]
    public class BlogCategoryMapping
    {
        [Required]
        [Column("blog_id")]
        public Guid BlogId { get; set; }

        [Required]
        [Column("category_id")]
        public int CategoryId { get; set; }

        // Navigation properties
        [ForeignKey(nameof(BlogId))]
        public virtual Blog? Blog { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public virtual BlogCategory? BlogCategory { get; set; }
    }
}
