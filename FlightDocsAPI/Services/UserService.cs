using FlightDocsAPI.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text;

namespace FlightDocsAPI.Services
{
    public class UserService : IUserService
{
    private readonly FlightDocsContext _context;
    private readonly string _secretKey = "J9S09Lv8YhqnI5OTpf0NqKjnHhc2oX6T";

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

    private string GeneratePasswordToken(string password)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_secretKey);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Hash, password) }),
            Expires = DateTime.UtcNow.AddYears(1), // Thời gian hết hạn token
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    // Phương thức giải mã token để lấy lại mật khẩu
    private string DecodePasswordToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_secretKey);
        var parameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false
        };

        var claimsPrincipal = tokenHandler.ValidateToken(token, parameters, out _);
        var passwordClaim = claimsPrincipal.FindFirst(ClaimTypes.Hash);
        return passwordClaim?.Value;
    }

    public async Task<UserDto> CreateUserAsync(UserDto userDto)
    {
        // Mã hóa mật khẩu
        var hashedPassword = GeneratePasswordToken(userDto.Password);

        var user = new User
        {
            Username = userDto.Username,
            Email = userDto.Email,
            Password = hashedPassword, // Lưu token mã hóa thay vì mật khẩu gốc
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
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        if (user == null) return null;

        // Giải mã mật khẩu đã lưu
        var decodedPassword = DecodePasswordToken(user.Password);

        // So sánh mật khẩu người dùng nhập với mật khẩu đã giải mã
        if (decodedPassword != password) return null;

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

        // Mã hóa mật khẩu mới
        user.Password = GeneratePasswordToken(newPassword);

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
