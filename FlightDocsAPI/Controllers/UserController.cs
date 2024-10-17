
using FlightDocsAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json; // Thêm dòng này


namespace FlightDocsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userService.GetUsersAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserDto userDto)
        {
            var createdUser = await _userService.CreateUserAsync(userDto);
            return CreatedAtAction(nameof(GetUserById), new { id = createdUser.Username }, createdUser);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var result = await _userService.DeleteUserAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] JsonElement loginData)
        {
            if (!loginData.TryGetProperty("email", out var emailElement) ||
                !loginData.TryGetProperty("password", out var passwordElement))
            {
                return BadRequest("Email and password are required.");
            }

            string email = emailElement.GetString();
            string password = passwordElement.GetString();
            var user = await _userService.AuthenticateAsync(email, password);
            if (user == null) return Unauthorized("Invalid email or password");

            return Ok(user);
        }

        [HttpPatch("change-username/{id}")]
        public async Task<IActionResult> ChangeUsername(int id, [FromBody] JsonElement body)
        {
            if (!body.TryGetProperty("username", out var newUsernameElement))
            {
                return BadRequest("New username is required.");
            }

            string newUsername = newUsernameElement.GetString();
            var result = await _userService.UpdateUsernameAsync(id, newUsername);
            if (!result) return NotFound("User not found or update failed.");

            return Ok("Username updated successfully.");
        }

        [HttpPatch("change-password/{id}")]
        public async Task<IActionResult> ChangePassword(int id, [FromBody] JsonElement body)
        {
            if (!body.TryGetProperty("password", out var newPasswordElement))
            {
                return BadRequest("New password is required.");
            }

            string newPassword = newPasswordElement.GetString();
            var result = await _userService.UpdatePasswordAsync(id, newPassword);
            if (!result) return NotFound("User not found or update failed.");

            return Ok("Password updated successfully.");
        }

        [HttpPatch("update-info/{id}")]
        public async Task<IActionResult> UpdateUserInfo(int id, [FromBody] JsonElement updateData)
        {
            var result = await _userService.UpdateUserInfoAsync(id, updateData);
            if (!result) return NotFound("User not found or update failed.");

            return Ok("User information updated successfully.");
        }
    }

}
