using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObject.Models.Entity
{
    [Table("products_recommendation")]
    public class ProductRecommendation
    {
        [Key]
        [Column("pair_id")]
        public Guid PairId { get; set; }

        [Column("product_id_a")]
        public Guid ProductIdA{ get; set; }

        [Column("product_id_b")]
        public Guid ProductIdB { get; set; }

        [Column("score")]
        public int Score { get; set; }

        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; }

        // Navigation properties
        [ForeignKey(nameof(ProductIdA))]
        public virtual Product? ProductA { get; set; }

        // Navigation properties
        [ForeignKey(nameof(ProductIdB))]
        public virtual Product? ProductB { get; set; }
    }
}
