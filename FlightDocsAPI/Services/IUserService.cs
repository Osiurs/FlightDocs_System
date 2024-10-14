using FlightDocsAPI.Models;
using System.Text.Json;

namespace FlightDocsAPI.Services
{
    public interface IUserService
    {
        Task<User> AuthenticateAsync(string email, string password);
        Task<IEnumerable<User>> GetUsersAsync();
        Task<User> GetUserByIdAsync(int id);
        Task<User> CreateUserAsync(User user);
        Task<bool> DeleteUserAsync(int id);
        Task<bool> UpdateUsernameAsync(int userId, string newUsername);
        Task<bool> UpdatePasswordAsync(int userId, string newPassword);
        Task<bool> UpdateUserInfoAsync(int userId, JsonElement updateData);

    }
}
