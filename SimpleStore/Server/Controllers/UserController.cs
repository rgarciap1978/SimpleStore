using Microsoft.AspNetCore.Mvc;
using SimpleStore.Services.Interfaces;
using SimpleStore.Shared.Request;

namespace SimpleStore.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UserController : Controller
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Login(RequestDTOLogin request)
        {
            var response = await _service.LoginAsync(request);
            return response.Success ? Ok(response) : StatusCode((int)StatusCodes.Status401Unauthorized, response);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RequestDTORegister request)
        {
            var response = await _service.RegisterAsync(request);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpPost]
        public async Task<IActionResult> SendToken(RequestDTOGenerateToken request)
        {
            return Ok(await _service.RequestTokenResetPaswordAsync(request));
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(RequestDTOResetPassword request)
        {
            var response = await _service.ResetPasswordAsync(request);
            return response.Success ? Ok(response) : BadRequest(response);
        }
    }
}
