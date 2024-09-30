using FlightDocsAPI.Models;

namespace FlightDocsAPI.Services
{
    public interface IUserService
    {
        Task<User> AuthenticateAsync(string email, string password);
        Task<IEnumerable<User>> GetUsersAsync();
        Task<User> GetUserByIdAsync(int id);
        Task<User> CreateUserAsync(User user);
        Task<bool> DeleteUserAsync(int id);
    }
}
