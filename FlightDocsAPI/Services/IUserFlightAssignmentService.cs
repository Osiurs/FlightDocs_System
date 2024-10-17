
using Microsoft.AspNetCore.JsonPatch;

namespace FlightDocsAPI.Services
{
    public interface IUserFlightAssignmentService
    {
        Task<IEnumerable<UserFlightAssignmentDto>> GetAssignmentsByUserIdAsync(int userId);
        Task<IEnumerable<UserFlightAssignmentDto>> GetAssignmentsByFlightIdAsync(int flightId);
        Task<UserFlightAssignmentDto> AssignUserToFlightAsync(UserFlightAssignmentDto assignmentDto);
        Task<bool> DeleteAssignmentAsync(int assignmentId);
        Task<bool> UpdateAssignmentAsync(int id, UserFlightAssignmentDto flightAssignmentDto);
    }

}
