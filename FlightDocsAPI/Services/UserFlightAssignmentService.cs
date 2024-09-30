using FlightDocsAPI.Data;
using FlightDocsAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightDocsAPI.Services
{
    public class UserFlightAssignmentService : IUserFlightAssignmentService
    {
        private readonly FlightDocsContext _context;

        public UserFlightAssignmentService(FlightDocsContext context)
        {
            _context = context;
        }

        // Lấy danh sách phân công theo UserID
        public async Task<IEnumerable<UserFlightAssignment>> GetAssignmentsByUserIdAsync(int userId)
        {
            return await _context.UserFlightAssignments
                .Where(a => a.UserID == userId)
                .ToListAsync();
        }

        // Lấy danh sách phân công theo FlightID
        public async Task<IEnumerable<UserFlightAssignment>> GetAssignmentsByFlightIdAsync(int flightId)
        {
            return await _context.UserFlightAssignments
                .Where(a => a.FlightID == flightId)
                .ToListAsync();
        }

        // Phân công người dùng vào chuyến bay
        public async Task<UserFlightAssignment> AssignUserToFlightAsync(UserFlightAssignment assignment)
        {
            _context.UserFlightAssignments.Add(assignment);
            await _context.SaveChangesAsync();
            return assignment;
        }

        // Xóa phân công
        public async Task<bool> DeleteAssignmentAsync(int assignmentId)
        {
            var assignment = await _context.UserFlightAssignments.FindAsync(assignmentId);
            if (assignment == null) return false;

            _context.UserFlightAssignments.Remove(assignment);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
