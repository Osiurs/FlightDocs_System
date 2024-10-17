using FlightDocsAPI.Data;

using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace FlightDocsAPI.Services
{
    public class UserService : IUserService
{
    private readonly FlightDocsContext _context;

    public UserService(FlightDocsContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<UserDto>> GetUsersAsync()
    {
        return await _context.Users
                             .Select(user => new UserDto
                             {
                                 Username = user.Username,
                                 Email = user.Email,
                                 Password = user.Password,
                                 Role = user.Role,
                                 FullName = user.FullName,
                                 PhoneNumber = user.PhoneNumber,
                                 Status = user.Status
                             }).ToListAsync();
    }

    public async Task<UserDto> GetUserByIdAsync(int userId)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user == null) return null;

        return new UserDto
        {
            Username = user.Username,
            Email = user.Email,
            Role = user.Role,
            FullName = user.FullName,
            PhoneNumber = user.PhoneNumber,
            Status = user.Status
        };
    }

    public async Task<UserDto> CreateUserAsync(UserDto userDto)
    {
        var user = new User
        {
            Username = userDto.Username,
            Email = userDto.Email,
            Password = userDto.Password,
            Role = userDto.Role,
            FullName = userDto.FullName,
            PhoneNumber = userDto.PhoneNumber,
            Status = userDto.Status,
            CreatedAt = DateTime.Now
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return userDto;
    }

    public async Task<bool> DeleteUserAsync(int userId)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user == null) return false;

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<UserDto> AuthenticateAsync(string email, string password)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
        if (user == null) return null;

        return new UserDto
        {
            Username = user.Username,
            Email = user.Email,
            Role = user.Role,
            FullName = user.FullName,
            PhoneNumber = user.PhoneNumber,
            Status = user.Status
        };
    }

    public async Task<bool> UpdateUsernameAsync(int userId, string newUsername)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user == null) return false;

        user.Username = newUsername;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UpdatePasswordAsync(int userId, string newPassword)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user == null) return false;

        user.Password = newPassword;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UpdateUserInfoAsync(int userId, JsonElement updateData)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user == null) return false;

        if (updateData.TryGetProperty("email", out var emailElement))
        {
            user.Email = emailElement.GetString();
        }

        if (updateData.TryGetProperty("fullName", out var fullNameElement))
        {
            user.FullName = fullNameElement.GetString();
        }

        if (updateData.TryGetProperty("phoneNumber", out var phoneNumberElement))
        {
            user.PhoneNumber = phoneNumberElement.GetString();
        }

        if (updateData.TryGetProperty("status", out var statusElement))
        {
            user.Status = statusElement.GetString();
        }

        await _context.SaveChangesAsync();
        return true;
    }
}

}
