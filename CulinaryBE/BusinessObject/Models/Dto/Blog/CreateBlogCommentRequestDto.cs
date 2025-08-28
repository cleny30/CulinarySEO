namespace BusinessObject.Models.Dto.Blog
{
    public class CreateBlogCommentRequestDto
    {

        public Guid BlogId { get; set; }

        public Guid CustomerId { get; set; }

        public string Content { get; set; } = string.Empty;

        public Guid? ParentCommentId { get; set; }
    }
}
