using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObject.Models.Entity
{
    [Table("blog_comments")]
    public class BlogComment
    {
        [Key]
        [Column("comment_id")]
        public Guid CommentId { get; set; }

        [Required]
        [Column("blog_id")]
        public Guid BlogId { get; set; }

        [Required]
        [Column("customer_id")]
        public Guid CustomerId { get; set; }

        [Required]
        [Column("content")]
        public string Content { get; set; } = string.Empty;

        [Column("parent_comment_id")]
        public Guid? ParentCommentId { get; set; }

        [Required]
        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        [ForeignKey(nameof(BlogId))]
        public virtual Blog? Blog { get; set; }

        [ForeignKey((nameof(CustomerId)))]
        public virtual Customer? Customer { get; set; }

        [ForeignKey(nameof(ParentCommentId))]
        public virtual BlogComment? ParentComment { get; set; }
        public virtual ICollection<BlogComment>? ChildComments { get; set; }
    }
}
