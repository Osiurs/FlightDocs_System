using FlightDocsAPI.Models;

namespace FlightDocsAPI.Services
{
    public interface IFlightService
    {
        Task<IEnumerable<Flight>> GetAllFlightsAsync();
        Task<Flight> GetFlightByIdAsync(int id);
        Task<Flight> CreateFlightAsync(Flight flight);
        Task<bool> DeleteFlightAsync(int id);
    }
}
