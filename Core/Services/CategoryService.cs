using Dapper;
using WebApplication1.Core.Contracts;
using WebApplication1.Infra;
using WebApplication1.Models;

namespace WebApplication1.Core.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly DataContext _context;
        public CategoryService(DataContext context)
        {
            _context = context;
        }
        public async Task<List<Category>> getAllCategory()
        {
            using var conn = _context.CreateConnection();

            var sql = "SELECT * FROM Categories";
            var post = await conn.QueryAsync<Category>(sql);
            return post.ToList();
        }
    }
}
