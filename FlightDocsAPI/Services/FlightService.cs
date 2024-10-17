using FlightDocsAPI.Data;
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

        public async Task<IEnumerable<FlightDto>> GetAllFlightsAsync()
        {
            return await _context.Flights
                .Select(f => new FlightDto
                {
                    FlightID = f.FlightID,
                    FlightNumber = f.FlightNumber,
                    DepartureTime = f.DepartureTime,
                    ArrivalTime = f.ArrivalTime,
                    Route = f.Route,
                    PlaneType = f.PlaneType,
                    Status = f.Status
                })
                .ToListAsync();
        }

        public async Task<FlightDto> GetFlightByIdAsync(int id)
        {
            var flight = await _context.Flights.FindAsync(id);
            if (flight == null) return null;

            return new FlightDto
            {
                FlightID = flight.FlightID,
                FlightNumber = flight.FlightNumber,
                DepartureTime = flight.DepartureTime,
                ArrivalTime = flight.ArrivalTime,
                Route = flight.Route,
                PlaneType = flight.PlaneType,
                Status = flight.Status
            };
        }

        public async Task<FlightDto> CreateFlightAsync(FlightDto flightDto)
        {
            var flight = new Flight
            {
                FlightNumber = flightDto.FlightNumber,
                DepartureTime = flightDto.DepartureTime,
                ArrivalTime = flightDto.ArrivalTime,
                Route = flightDto.Route,
                PlaneType = flightDto.PlaneType,
                Status = flightDto.Status,
                CreatedAt = DateTime.Now // Có thể không cần vì đã có giá trị mặc định trong Entity
            };

            _context.Flights.Add(flight);
            await _context.SaveChangesAsync();

            flightDto.FlightID = flight.FlightID; // Gán ID vào DTO

            return flightDto;
        }

        public async Task<bool> DeleteFlightAsync(int id)
        {
            var flight = await _context.Flights.FindAsync(id);
            if (flight == null) return false;

            _context.Flights.Remove(flight);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UpdateFlightAsync(int id, FlightDto flightDto)
    {
        var flight = await _context.Flights.FindAsync(id);
        if (flight == null) return false;

        // Chỉ cập nhật các trường có giá trị trong flightDto
        if (flightDto.FlightNumber != null)
        {
            flight.FlightNumber = flightDto.FlightNumber;
        }

        if (flightDto.DepartureTime != DateTime.MinValue)
        {
            flight.DepartureTime = flightDto.DepartureTime;
        }

        if (flightDto.ArrivalTime != DateTime.MinValue)
        {
            flight.ArrivalTime = flightDto.ArrivalTime;
        }

        if (flightDto.Route != null)
        {
            flight.Route = flightDto.Route;
        }

        if (flightDto.PlaneType != null)
        {
            flight.PlaneType = flightDto.PlaneType;
        }

        if (flightDto.Status != null)
        {
            flight.Status = flightDto.Status;
        }

        _context.Flights.Update(flight);
        await _context.SaveChangesAsync();

        return true;
    }
    }


}
