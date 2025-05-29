using WebApplication1.Models;

namespace WebApplication1.Core.Contracts
{
    public interface ICategoryService
    {
        public Task<List<Category>> getAllCategory();
    }
}
