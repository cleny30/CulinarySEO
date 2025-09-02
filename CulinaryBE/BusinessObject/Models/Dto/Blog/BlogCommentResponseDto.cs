namespace BusinessObject.Models.Dto.Blog
{
    public class BlogCommentResponseDto
    {
        public Guid CommentId { get; set; }
        public Guid BlogId { get; set; }
        public Guid CustomerId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public Guid? ParentCommentId { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<BlogCommentResponseDto>? Replies { get; set; }
    }
}
