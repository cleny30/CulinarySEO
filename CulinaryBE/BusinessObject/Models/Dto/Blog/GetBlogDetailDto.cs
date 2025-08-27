namespace BusinessObject.Models.Dto.Blog
{
    public class GetBlogDetailDto
    {
        public Guid BlogId { get; set; }

        public Guid ManagerId { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;

        public string ImageTitle { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }

        public string ManagerName { get; set; } = string.Empty;

        public List<BlogCategories> Categories { get; set; } = new List<BlogCategories>();
        public List<GetBlogCommentDto> Comments { get; set; } = new List<GetBlogCommentDto>();

    }
}
