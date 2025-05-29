using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;
using System.Linq.Expressions;
using WebApplication1.Core.Contracts;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("post")]
    [ApiController]
    public class BlogPostController : ControllerBase
    {
        private readonly IBlogService _blogService;
        public BlogPostController(IBlogService blogService)
        {
            _blogService = blogService;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<BlogPost>> GetSingleBlogPost(int id)
        {
            try
            {
                var result = await _blogService.GetSingleBlog(id);
                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error occurred.");
            }

        }

        [HttpGet]
        public async Task<ActionResult<List<BlogPost>>> GetAllBlogPost()
        {
            try
            {
                var result = await _blogService.GetAllBlogs();
                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        //[HttpGet]
        //public async Task<ActionResult<List<BlogPost>>> GetBlogByCategoryId([FromQuery] int categoryId)
        //{
        //    try
        //    {
        //        throw new NotImplementedException();
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, "An unexpected error occurred.");
        //    }
        //}

        [HttpPost]
        public async Task<ActionResult<BlogPost>> CreatePost([FromBody] BlogPostDto post)
        {

            try
            {
                var result = await _blogService.CreateBlogs(post);
                return Ok(result); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error occurred.");
            }

        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BlogPost>> UpdatePost(int id, [FromBody] BlogPostDto post)
        {
            try
            {
                var result = await _blogService.UpdateBlog(post, id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePost(int id)
        {
            try
            {
                await _blogService.DeleteSingleBlog(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error occurred.");
            }

        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAllPost()
        {
            try
            {
                await _blogService.DeleteAllBlog();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error occurred.");
            }

        }

        
    }
}