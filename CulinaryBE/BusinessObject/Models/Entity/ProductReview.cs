using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObject.Models.Entity
{
    [Table("product_reviews")]
    public class ProductReview
    {
        [Key]
        [Column("review_id")]
        public Guid ReviewId { get; set; }

        [Required]
        [Column("product_id")]
        public Guid ProductId { get; set; }

        [Required]
        [Column("customer_id")]
        public Guid CustomerId { get; set; }

        [Column("rating")]
        public int? Rating { get; set; }

        [Required]
        [Column("comment")]
        public string Comment { get; set; } = string.Empty;

        [Required]
        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        [ForeignKey("ProductId")]
        public virtual Product? Product { get; set; } 

        [ForeignKey("CustomerId")]
        public virtual Customer? Customer { get; set; }
    }
}
