using FlightDocsAPI.Data;
using FlightDocsAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightDocsAPI.Services
{
    public class FlightService : IFlightService
    {
        private readonly FlightDocsContext _context;

        public FlightService(FlightDocsContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Flight>> GetAllFlightsAsync()
        {
            return await _context.Flights.ToListAsync();
        }

        public async Task<Flight> GetFlightByIdAsync(int id)
        {
            return await _context.Flights.FindAsync(id);
        }

        public async Task<Flight> CreateFlightAsync(Flight flight)
        {
            _context.Flights.Add(flight);
            await _context.SaveChangesAsync();
            return flight;
        }

        public async Task<bool> DeleteFlightAsync(int id)
        {
            var flight = await _context.Flights.FindAsync(id);
            if (flight == null) return false;

            _context.Flights.Remove(flight);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
