using FlightDocsAPI.Models;
using FlightDocsAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace FlightDocsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserFlightAssignmentController : ControllerBase
    {
        private readonly IUserFlightAssignmentService _assignmentService;

        public UserFlightAssignmentController(IUserFlightAssignmentService assignmentService)
        {
            _assignmentService = assignmentService;
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetAssignmentsByUserId(int userId)
        {
            var assignments = await _assignmentService.GetAssignmentsByUserIdAsync(userId);
            return Ok(assignments);
        }

        [HttpGet("flight/{flightId}")]
        public async Task<IActionResult> GetAssignmentsByFlightId(int flightId)
        {
            var assignments = await _assignmentService.GetAssignmentsByFlightIdAsync(flightId);
            return Ok(assignments);
        }

        [HttpPost]
        public async Task<IActionResult> AssignUserToFlight([FromBody] UserFlightAssignment assignment)
        {
            var result = await _assignmentService.AssignUserToFlightAsync(assignment);
            return CreatedAtAction(nameof(GetAssignmentsByUserId), new { userId = result.UserID }, result);
        }

        [HttpDelete("{assignmentId}")]
        public async Task<IActionResult> DeleteAssignment(int assignmentId)
        {
            var result = await _assignmentService.DeleteAssignmentAsync(assignmentId);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
