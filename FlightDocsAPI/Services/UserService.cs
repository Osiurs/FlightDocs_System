using FlightDocsAPI.Data;
using FlightDocsAPI.Models;
using Microsoft.EntityFrameworkCore;

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
    }
}
