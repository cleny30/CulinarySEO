namespace BusinessObject.Models.Dto.Blog
{
    public class GetBlogCommentDto
    {
        public Guid CommentId { get; set; }
        public Guid BlogId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public Guid? ParentCommentId { get; set; }
        public DateTime CreatedAt { get; set; }

        public int ChildCommentCount { get; set; }
        public List<GetBlogCommentDto> Replies { get; set; } = new List<GetBlogCommentDto>();
    }
}