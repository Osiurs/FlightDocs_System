public class Flight
{
    public int FlightID { get; set; }
    public string FlightNumber { get; set; }
    public DateTime DepartureTime { get; set; }
    public DateTime ArrivalTime { get; set; }
    public string Route { get; set; }
    public string PlaneType { get; set; }
    public string Status { get; set; }
    public DateTime CreatedAt { get; set; }
    
    public ICollection<Document> Documents { get; set; }
    public ICollection<UserFlightAssignment> UserFlightAssignments { get; set; }
}
