namespace BusinessObject.Models.Dto.Blog
{
    public class GetBlogDto
    {
        public Guid BlogId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string ManagerName { get; set; }
        public string ImageTitle { get; set; }
        public List<BlogCategories> Categories { get; set; } = new List<BlogCategories>();
    }
}
