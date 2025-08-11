using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObject.Models.Entity
{
    [Table("products")]
    public class Product
    {
        [Key]
        [Column("product_id")]
        public Guid ProductId { get; set; }

        [Required]
        [MaxLength(100)]
        [Column("product_name")]
        public string ProductName { get; set; } = string.Empty;

        [Required]
        [Column("description")]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Column("price", TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }

        [Column("discount", TypeName = "decimal(5,2)")]
        public decimal? Discount { get; set; }

        [Required]
        [Column("category_id")]
        public int CategoryId { get; set; }

        [Required]
        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; }

        // Navigation properties
        [ForeignKey(nameof(CategoryId))]
        public virtual Category? Category { get; set; }
        public virtual ICollection<ProductImagesEmbedding>? ProductImagesEmbeddings { get; set; }
        public virtual ICollection<ProductImage>? ProductImages { get; set; }
        public virtual ICollection<ProductHistory>? ProductHistories { get; set; }
        public virtual ICollection<Stock>? Stocks { get; set; }
        public virtual ICollection<StockTransaction>? StockTransactions { get; set; }
        public virtual ICollection<CartItem>? CartItems { get; set; }
        public virtual ICollection<OrderDetail>? OrderDetails { get; set; }
        public virtual ICollection<ProductReview>? ProductReviews { get; set; }
    }
}
