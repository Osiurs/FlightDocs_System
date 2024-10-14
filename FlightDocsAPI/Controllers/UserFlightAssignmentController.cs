using FlightDocsAPI.Models;
using FlightDocsAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;

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
            if (assignment == null)
            {
                return BadRequest("Assignment cannot be null.");
            }

            // Gọi service để thêm assignment
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
        [HttpPut("{id}")]
        public async Task<IActionResult> PatchAssignmentAsync(int id, [FromBody] UserFlightAssignment flightAssignment)
        {
            if (id != flightAssignment.AssignmentID)
            {
                return BadRequest("UserFlightAssignment ID mismatch.");
            }

            var result = await _assignmentService.PatchAssignmentAsync(id, flightAssignment);

            if (result == null)
            {
                return NotFound("UserFlightAssignment not found or update failed.");
            }

            return Ok(flightAssignment);
        }

    }
}
