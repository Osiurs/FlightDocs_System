public class User
{
    public int UserID { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
    public string FullName { get; set; }
    public string PhoneNumber { get; set; }
    public string Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public ICollection<UserFlightAssignment> UserFlightAssignments { get; set; }
}
