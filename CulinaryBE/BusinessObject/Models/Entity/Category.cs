using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObject.Models.Entity
{
    [Table("categories")]
    public class Category
    {
        [Key]
        [Column("category_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryId { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("category_name")]
        public string CategoryName { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        [Column("category_image")]
        public string CategoryImage { get; set; } = string.Empty;

        [Required]
        [Column("description")]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Column("slug")]
        public string Slug { get; set; } = string.Empty;
        [Required]
        [Column("page_title")]
        public string PageTitle { get; set; } = string.Empty;
        [Required]
        [Column("meta_description")]
        public string MetaDescription { get; set; } = string.Empty;

        [Required]
        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


        // Navigation properties
        public virtual ICollection<ProductCategoryMapping>? ProductCategoryMappings { get; set; }

        [NotMapped]
        public int ProductCount { get; set; }
    }
}
