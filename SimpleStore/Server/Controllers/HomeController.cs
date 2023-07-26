using Microsoft.AspNetCore.Mvc;
using SimpleStore.Services.Implementations;
using SimpleStore.Services.Interfaces;
using SimpleStore.Shared.Response;

namespace SimpleStore.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : Controller
    {
        
        private readonly IProductService _productService;

        public HomeController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IResult> GetAllAsync()
        {
            try
            {
                return Results.Ok(await _productService.ListAsync(null, 1, 50));
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        }

        [HttpGet()]
        [Route("{id:int}")]
        public async Task<IActionResult> GetByCategoryIdAsync(int id)
        {
            try
            {
                return Ok(await _productService.ListAsync(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
