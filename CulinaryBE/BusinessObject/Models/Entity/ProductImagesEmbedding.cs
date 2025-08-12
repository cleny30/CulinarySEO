using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Pgvector;

namespace BusinessObject.Models.Entity
{
    [Table("product_images_embedding")]
    public class ProductImagesEmbedding
    {
        [Key]
        [Column("embedding_id")]
        public Guid EmbeddingId { get; set; }

        [Column("product_id")]
        public Guid ProductId { get; set; }

        [Required]
        [Column("image_id")]
        public Guid ImageId { get; set; }

        [Required]
        [Column("image_embedding_yolo", TypeName = "vector(3)")]
        public Vector ImageEmbeddingYolo { get; set; } = new Vector(new float[3]);

        [Required]
        [MaxLength(768)]
        [Column("description_embed")]
        public string DescriptionEmbed { get; set; } = string.Empty;

        // Navigation properties
        [ForeignKey(nameof(ProductId))]
        public virtual Product? Product { get; set; }

        [ForeignKey(nameof(ImageId))]
        public virtual ProductImage? ProductImage { get; set; }
    }
}
