using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObject.Models.Entity
{
    [Table("product_embedding")]
    public class ProductEmbedding
    {
        [Key]
        [Column("product_id")]
        public Guid ProductId { get; set; }

        [Required]
        [MaxLength(512)]
        [Column("images_embed_yolo")]
        public string ImagesEmbedYolo { get; set; } = string.Empty;

        [Required]
        [MaxLength(768)]
        [Column("images_embed_clip")]
        public string ImagesEmbedClip { get; set; } = string.Empty;

        [Required]
        [MaxLength(768)]
        [Column("description_embed")]
        public string DescriptionEmbed { get; set; } = string.Empty;

        // Navigation properties
        [ForeignKey(nameof(ProductId))]
        public virtual Product? Product { get; set; }
    }
}
