using Dapper;
using Microsoft.Extensions.Hosting;
using WebApplication1.Core.Contracts;
using WebApplication1.Infra;
using WebApplication1.Models;

namespace WebApplication1.Core.Services
{
    public class BlogService : IBlogService
    {
        private readonly DataContext _context;
        public BlogService(DataContext context)
        {
            _context = context;
        }

        public async Task<BlogPost> CreateBlogs(BlogPostDto postDto)
        {
            using var conn = _context.CreateConnection();
            var post = new BlogPost()
            {
                Title = postDto.Title,
                Contents = postDto.Contents,
                CategoryId = postDto.CategoryId,
                TimeStamp = DateTime.UtcNow
            };
            const string sql = @"
            INSERT INTO BlogPosts (Title, Contents, Timestamp, CategoryId)
            VALUES (@Title, @Contents, @TimeStamp, @CategoryId);";

            var newId = await conn.ExecuteScalarAsync<long>(sql, post);
            return post;
        }

        public async Task DeleteSingleBlog(int id)
        {
            const string sql = "DELETE FROM BlogPosts WHERE Id = @id";
            var parameters = new DynamicParameters();
            parameters.Add("id", id);
            using var conn = _context.CreateConnection();
            var affectedRows = await conn.ExecuteAsync(sql, parameters);
        }

        public async Task<BlogPost> UpdateBlog(BlogPostDto blogPostDto, int id)
        {
            const string sql = @"
        UPDATE BlogPosts
        SET Title = @Title,
            Contents = @Contents,
            CategoryId = @CategoryId
        WHERE Id = @Id";

            var post = new BlogPost()
            {
                Id = id,
                Title = blogPostDto.Title,
                Contents = blogPostDto.Contents,
                CategoryId = blogPostDto.CategoryId,
                TimeStamp= DateTime.UtcNow
            };

            using var conn = _context.CreateConnection();
            var affectedRows = await conn.ExecuteAsync(sql, post);
            return post;
        }

        public async Task<BlogPost?> GetSingleBlog(int id)
        {
            using var conn = _context.CreateConnection();

            //await test();
            var sql = "SELECT * FROM BlogPosts WHERE Id = @id";
            var parameters = new DynamicParameters();
            parameters.Add("id", id);
            var post = await conn.QueryFirstOrDefaultAsync<BlogPost>(sql, parameters);
            return post;
        }
        
        public async Task<List<BlogPost>?> GetAllBlogs()
        {
            using var conn = _context.CreateConnection();

            var sql = "SELECT * FROM BlogPosts";
            var post = await conn.QueryAsync<BlogPost>(sql);
            return post.ToList();
        }

        public async Task DeleteAllBlog()
        {
            const string sql = "DELETE FROM BlogPosts";
            using var conn = _context.CreateConnection();
            var affectedRows = await conn.ExecuteAsync(sql);
        }
    }
}
