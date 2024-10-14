using FlightDocsAPI.Data;
using FlightDocsAPI.Models;
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
            await _context.UserFlightAssignments.AddAsync(assignment);
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
       public async Task<bool> PatchAssignmentAsync(int assignmentId, UserFlightAssignment updatedAssignment)
        {
            // Tìm assignment theo assignmentId
            var assignment = await _context.UserFlightAssignments.FindAsync(assignmentId);
            if (assignment == null)
            {
                return false;
            }

            // Cập nhật thông tin cho assignment
            assignment.UserID = updatedAssignment.UserID;
            assignment.FlightID = updatedAssignment.FlightID;
            assignment.RoleOnFlight = updatedAssignment.RoleOnFlight; // Cập nhật thuộc tính cần thiết
            assignment.AssignmentDate = updatedAssignment.AssignmentDate;

            // Lưu thay đổi vào cơ sở dữ liệu
            _context.UserFlightAssignments.Update(assignment);
           var updated = await _context.SaveChangesAsync();

            return updated > 0; // Trả về assignment đã cập nhật
        }
    }
}
