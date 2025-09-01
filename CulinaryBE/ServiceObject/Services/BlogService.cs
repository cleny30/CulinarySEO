
using AutoMapper;
using BusinessObject.Models.Dto.Blog;
using BusinessObject.Models.Entity;
using DataAccess.IDAOs;
using ServiceObject.IServices;

namespace ServiceObject.Services
{
    public class BlogService : IBlogService
    {
        private readonly IBlogDAO _blogDAO;
        private readonly IMapper _mapper;

        public BlogService(IBlogDAO blogDAO, IMapper mapper)
        {
            _blogDAO = blogDAO;
            _mapper = mapper;
        }

        public async Task<GetBlogDetailDto?> GetBlogById(Guid blogId)
        {
            var blog = await _blogDAO.GetBlogById(blogId);
            return _mapper.Map<GetBlogDetailDto>(blog);
        }

        public async Task<List<GetBlogDto>> GetBlogs()
        {
            var blogs = await _blogDAO.GetAllBlogs();
            return _mapper.Map<List<GetBlogDto>>(blogs);
        }

        public async Task AddComment(CreateBlogCommentRequestDto request, Guid customerId)
        {
            var comment = _mapper.Map<BlogComment>(request);
            comment.CustomerId = customerId;
            await _blogDAO.AddComment(comment);
        }

        public async Task<List<BlogCommentResponseDto>> GetCommentsByBlogId(Guid blogId)
        {
            var comments = await _blogDAO.GetCommentsByBlogId(blogId);
            return _mapper.Map<List<BlogCommentResponseDto>>(comments);
        }
    }
}
