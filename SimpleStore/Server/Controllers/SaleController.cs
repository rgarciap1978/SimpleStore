using Microsoft.AspNetCore.Mvc;
using SimpleStore.Services.Implementations;
using SimpleStore.Services.Interfaces;
using SimpleStore.Shared.Request;
using System.Security.Claims;

namespace SimpleStore.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SaleController : Controller
    {
        private readonly ISaleService _service;
        private readonly ILogger<SaleService> _logger;

        public SaleController(
            ISaleService service,
            ILogger<SaleService> logger
            )
        {
            _service = service;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RequestDTOSale request)
        {
            var email = HttpContext.User.Claims.First(f => f.Type == ClaimTypes.Email).Value;
            var response = await _service.AddAsync(email, request);
            return Ok(response);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var response = await _service.GetAsync(id);
            return response.Success ? Ok(response) : NotFound(response);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> ListSale(string? filter, int page=1, int rows = 10)
        {
            var email = HttpContext.User.Claims.First(f => f.Type == ClaimTypes.Email).Value;
            var response = await _service.ListAsync(email, filter, page, rows);
            return response.Success ? Ok(response) : NotFound(response);
        }
    }
}
