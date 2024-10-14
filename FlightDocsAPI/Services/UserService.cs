using FlightDocsAPI.Data;
using FlightDocsAPI.Models;
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

        public async Task<User> AuthenticateAsync(string email, string password)
        {
            // Kiểm tra email thuộc domain VietjetAir
            if (!email.EndsWith("@vietjetair.com"))
            {
                return null;
            }

            // Kiểm tra mật khẩu
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == email && u.Password == password);
            return user;
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User> CreateUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UpdateUsernameAsync(int userId, string newUsername)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null) return false;

            user.Username = newUsername; // Cập nhật Username
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdatePasswordAsync(int userId, string newPassword)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null) return false;

            user.Password = newPassword; // Cập nhật Password
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UpdateUserInfoAsync(int userId, JsonElement updateData)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null) return false;

            // Cập nhật các thông tin có trong updateData
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

            // Lưu thay đổi vào cơ sở dữ liệu
            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return true;
        }

    }
}
