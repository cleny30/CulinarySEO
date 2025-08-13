using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObject.Models.Entity
{
    [Table("product_category_mappings")]
    public class ProductCategoryMapping
    {
        [Required]
        [Column("product_id")]
        public Guid ProductId { get; set; }

        [Required]
        [Column("category_id")]
        public int CategoryId { get; set; }

        // Navigation properties
        [ForeignKey(nameof(ProductId))]
        public virtual Product? Product { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public virtual Category? Category { get; set; }
    }
}
