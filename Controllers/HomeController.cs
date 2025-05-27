using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication1.Core.Contracts;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("/")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IProcessorService _processorService;
        public HomeController(IProcessorService processorService) 
        {
            _processorService = processorService;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> getNathanValue(string id)
        {
            var result = await _processorService.ProcessMessage(id);
            if(!string.IsNullOrEmpty(result)) 
            {
                return Ok("aaa");
            }
            return BadRequest("Cant find record");
            
        }
    }
}