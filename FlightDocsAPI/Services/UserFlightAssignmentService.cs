using FlightDocsAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.JsonPatch;

namespace FlightDocsAPI.Services
{
    public class UserFlightAssignmentService : IUserFlightAssignmentService
{
    private readonly FlightDocsContext _context;

    public UserFlightAssignmentService(FlightDocsContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<UserFlightAssignmentDto>> GetAssignmentsByUserIdAsync(int userId)
    {
        return await _context.UserFlightAssignments
            .Where(a => a.UserID == userId)
            .Select(a => new UserFlightAssignmentDto
            {
                UserID = a.UserID,
                FlightID = a.FlightID,
                RoleOnFlight = a.RoleOnFlight,
                AssignmentDate = a.AssignmentDate
            })
            .ToListAsync();
    }

    public async Task<IEnumerable<UserFlightAssignmentDto>> GetAssignmentsByFlightIdAsync(int flightId)
    {
        return await _context.UserFlightAssignments
            .Where(a => a.FlightID == flightId)
            .Select(a => new UserFlightAssignmentDto
            {
                UserID = a.UserID,
                FlightID = a.FlightID,
                RoleOnFlight = a.RoleOnFlight,
                AssignmentDate = a.AssignmentDate
            })
            .ToListAsync();
    }

    public async Task<UserFlightAssignmentDto> AssignUserToFlightAsync(UserFlightAssignmentDto assignmentDto)
    {
        var assignment = new UserFlightAssignment
        {
            UserID = assignmentDto.UserID,
            FlightID = assignmentDto.FlightID,
            RoleOnFlight = assignmentDto.RoleOnFlight,
            AssignmentDate = assignmentDto.AssignmentDate
        };

        _context.UserFlightAssignments.Add(assignment);
        await _context.SaveChangesAsync();

        return assignmentDto;
    }

    public async Task<bool> DeleteAssignmentAsync(int assignmentId)
    {
        var assignment = await _context.UserFlightAssignments.FindAsync(assignmentId);
        if (assignment == null) return false;

        _context.UserFlightAssignments.Remove(assignment);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UpdateAssignmentAsync(int id, UserFlightAssignmentDto flightAssignmentDto)
    {
        var assignment = await _context.UserFlightAssignments.FindAsync(id);
        if (assignment == null) return false;

        assignment.RoleOnFlight = flightAssignmentDto.RoleOnFlight;
        assignment.AssignmentDate = flightAssignmentDto.AssignmentDate;

        _context.UserFlightAssignments.Update(assignment);
        await _context.SaveChangesAsync();

        return true;
    }
}

}
