
using AutoMapper;
using BusinessObject.Models.Dto.Blog;
using DataAccess.IDAOs;
using ServiceObject.IServices;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        public async Task<List<GetBlogDto>> GetBlogs()
        {
            var blogs = await _blogDAO.GetAllBlogs();
            return _mapper.Map<List<GetBlogDto>>(blogs);
        }
    }
}
