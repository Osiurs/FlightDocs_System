using FlightDocsAPI.Models;
using FlightDocsAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace FlightDocsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthenticationController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await _userService.AuthenticateAsync(model.Email, model.Password);
            if (user == null) return Unauthorized("Invalid email or password");

            return Ok(user);  // Trả về token hoặc thông tin user
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            // Xử lý đăng xuất
            return Ok("Logged out");
        }
    }
}
