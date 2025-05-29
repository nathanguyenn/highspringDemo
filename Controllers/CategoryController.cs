using Microsoft.AspNetCore.Mvc;
using WebApplication1.Core.Contracts;
using WebApplication1.Core.Services;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("categories")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet]
        public async Task<ActionResult<List<Category>>> GetAllCategories()
        {

            try
            {
                var result = await _categoryService.getAllCategory();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error occurred.");
            }

        }
    }
}
