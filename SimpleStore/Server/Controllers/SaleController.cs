using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleStore.Services;
using SimpleStore.Services.Implementations;
using SimpleStore.Services.Interfaces;
using SimpleStore.Shared.Request;
using SimpleStore.Shared.Response;
using System.Security.Claims;

namespace SimpleStore.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
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
        public async Task<IActionResult> ListSales(string? filter, int page = 1, int rows = 10)
        {
            var email = HttpContext.User.Claims.First(f => f.Type == ClaimTypes.Email).Value;
            var response = await _service.ListAsync(email, filter, page, rows);
            return response.Success ? Ok(response) : NotFound(response);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> ListSalesByDateRange(string dateStart, string dateEnd, int page, int rows)
        {
            try
            {
                var email = HttpContext.User.Claims.First(f => f.Type == ClaimTypes.Email).Value;
                var response = await _service.ListAsync(email, DateTime.Parse(dateStart), DateTime.Parse(dateEnd), page, rows);
                return response.Success ? Ok(response) : NotFound(response);
            }
            catch (Exception ex)
            {
                _logger.LogMessage(ex, nameof(ListSalesByDateRange));
                return BadRequest(new ResponseBase { Message = ex.Message });
            }
        }
    }
}
