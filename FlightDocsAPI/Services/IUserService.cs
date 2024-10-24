
using System.Text.Json;

namespace FlightDocsAPI.Services
{
    public interface IUserService
{
    Task<IEnumerable<UserDto>> GetUsersAsync();
    Task<UserDto> GetUserByIdAsync(int userId);
    Task<UserDto> CreateUserAsync(UserDto userDto);
    Task<bool> DeleteUserAsync(int userId);
    Task<UserDto> AuthenticateAsync(string email, string password);
    Task<bool> UpdatePasswordAsync(int userId, string newPassword);
    Task<bool> UpdateUserInfoAsync(int userId, JsonElement updateData);
}

}
