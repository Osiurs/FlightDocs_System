
using FlightDocsAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace FlightDocsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FlightController : ControllerBase
    {
        private readonly IFlightService _flightService;

        public FlightController(IFlightService flightService)
        {
            _flightService = flightService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFlights()
        {
            var flights = await _flightService.GetAllFlightsAsync();
            return Ok(flights);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFlight(int id)
        {
            var flight = await _flightService.GetFlightByIdAsync(id);
            if (flight == null) return NotFound();
            return Ok(flight);
        }

        [HttpPost]
        public async Task<IActionResult> CreateFlight([FromBody] FlightDto flightDto)
        {
            var createdFlight = await _flightService.CreateFlightAsync(flightDto);
            return CreatedAtAction(nameof(GetFlight), new { id = createdFlight.FlightID }, createdFlight);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFlight(int id)
        {
            var result = await _flightService.DeleteFlightAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateFlightAsync(int id, [FromBody] FlightDto flightDto)
        {
            var result = await _flightService.UpdateFlightAsync(id, flightDto);
            if (!result) return NotFound("Flight not found or update failed.");

            return Ok("Flight information updated successfully.");
        }
    }

}
