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
        public async Task<IActionResult> AssignUserToFlight([FromBody] UserFlightAssignmentDto assignmentDto)
        {
            if (assignmentDto == null)
            {
                return BadRequest("Assignment data is null.");
            }

            var result = await _assignmentService.AssignUserToFlightAsync(assignmentDto);
            return CreatedAtAction(nameof(GetAssignmentsByUserId), new { userId = result.UserID }, result);
        }

        [HttpDelete("{assignmentId}")]
        public async Task<IActionResult> DeleteAssignment(int assignmentId)
        {
            var result = await _assignmentService.DeleteAssignmentAsync(assignmentId);
            if (!result) return NotFound();
            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateAssignment(int id, [FromBody] UserFlightAssignmentDto assignmentDto)
        {
            if (!await _assignmentService.UpdateAssignmentAsync(id, assignmentDto))
            {
                return NotFound("Assignment not found or update failed.");
            }

            return Ok("Assignment updated successfully.");
        }
    }

}
