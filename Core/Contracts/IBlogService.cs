using WebApplication1.Models;

namespace WebApplication1.Core.Contracts
{
    public interface IBlogService
    {
        Task<BlogPost> GetSingleBlog(int id);
        Task<List<BlogPost>?> GetAllBlogs(int? categoryId);
        Task<BlogPost> UpdateBlog(BlogPostDto blogPostDto, int id);
        Task<BlogPost> CreateBlogs(BlogPostDto post);
        Task DeleteSingleBlog(int id);
        Task DeleteAllBlog();
    }
}
