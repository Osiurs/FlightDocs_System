public class UserFlightAssignment
{
    public int AssignmentID { get; set; }
    public int UserID { get; set; }
    public int FlightID { get; set; }
    public string RoleOnFlight { get; set; }
    public DateTime AssignmentDate { get; set; }

    // Nếu có quan hệ với các entity khác, hãy khai báo chúng.
    public virtual User User { get; set; }
    public virtual Flight Flight { get; set; }
}
