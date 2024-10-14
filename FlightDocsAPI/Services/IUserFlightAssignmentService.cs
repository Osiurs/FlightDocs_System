using FlightDocsAPI.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace FlightDocsAPI.Services
{
    public interface IUserFlightAssignmentService
    {
        Task<IEnumerable<UserFlightAssignment>> GetAssignmentsByUserIdAsync(int userId);
        Task<IEnumerable<UserFlightAssignment>> GetAssignmentsByFlightIdAsync(int flightId);
        Task<UserFlightAssignment> AssignUserToFlightAsync(UserFlightAssignment assignment);
        Task<bool> DeleteAssignmentAsync(int assignmentId);
         Task<bool> PatchAssignmentAsync(int assignmentId, UserFlightAssignment updatedAssignment);

    }
}
