public class FlightService : IFlightService
{
    private readonly FlightDbContext _context;

    public FlightService(FlightDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Flight>> GetAllFlights()
    {
        return await _context.Flights.ToListAsync();
    }

    public async Task<Flight> GetFlightById(int id)
    {
        return await _context.Flights.FindAsync(id);
    }

    public async Task AddFlight(Flight flight)
    {
        _context.Flights.Add(flight);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateFlight(int id, Flight flight)
    {
        var existingFlight = await _context.Flights.FindAsync(id);
        if (existingFlight != null)
        {
            existingFlight.FlightNumber = flight.FlightNumber;
            existingFlight.DepartureTime = flight.DepartureTime;
            existingFlight.ArrivalTime = flight.ArrivalTime;
            existingFlight.Status = flight.Status;
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteFlight(int id)
    {
        var flight = await _context.Flights.FindAsync(id);
        if (flight != null)
        {
            _context.Flights.Remove(flight);
            await _context.SaveChangesAsync();
        }
    }
}
