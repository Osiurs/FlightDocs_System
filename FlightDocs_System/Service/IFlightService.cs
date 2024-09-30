public interface IFlightService
{
    Task<IEnumerable<Flight>> GetAllFlights();
    Task<Flight> GetFlightById(int id);
    Task AddFlight(Flight flight);
    Task UpdateFlight(int id, Flight flight);
    Task DeleteFlight(int id);
}
