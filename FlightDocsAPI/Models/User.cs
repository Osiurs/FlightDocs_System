namespace FlightDocsAPI.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }  // Thêm thuộc tính Password
        public string Role { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Status { get; set; } = "Active";
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public ICollection<UserFlightAssignment> Assignments { get; set; }
    }
}
