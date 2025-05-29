using Dapper;
using WebApplication1.Core.Contracts;
using WebApplication1.Infra;
using WebApplication1.Models;

namespace WebApplication1.Core.Services
{
    public class ProcessorService : IProcessorService
    {
        private readonly DataContext _context;
        public ProcessorService(DataContext context)
        {
            _context = context;
        }
        public async Task<string>ProcessMessage(string id)
        {
            await test();
            var query = "SELECT * FROM Products";
            using var conn = _context.CreateConnection();
            var products = await conn.QueryAsync<Product>(query);
            return products.ToString();
        }

        private async Task test()
        {
            var product = new Product()
            {
                Id = 1,
                Name = "random name",
                Price = 100,

            };

            var query = "INSERT INTO Products (Name, Price) VALUES (@Name, @Price); SELECT last_insert_rowid();";
            using var conn = _context.CreateConnection();
            var id = await conn.ExecuteScalarAsync<int>(query, product);
        }
    }
}
