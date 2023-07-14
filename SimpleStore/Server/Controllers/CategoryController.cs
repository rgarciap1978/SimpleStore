using Azure;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SimpleStore.Services.Interfaces;
using SimpleStore.Shared.Request;

namespace SimpleStore.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _service;

        public CategoryController(ICategoryService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string? filter, int page = 1, int rows = 5)
        {
            return Ok(await _service.ListAsync(filter, page, rows));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var response = await _service.FindByIdAsync(id);
            return response.Success ? Ok(response) : NotFound(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post(RequestDTOCategory request)
        {
            var response = await _service.AddAsync(request);
            return CreatedAtAction(nameof(Get), new { id = response.Data }, response);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, RequestDTOCategory request)
        {
            return Ok(await _service.UpdateAsync(id, request));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _service.DeleteAsync(id));
        }
    }
}
