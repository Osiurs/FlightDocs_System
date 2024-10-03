namespace FlightDocsAPI.Models
{
    public class Flight
    {
        public int FlightID { get; set; }
        public string FlightNumber { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public string Route { get; set; }
        public string PlaneType { get; set; }
        public string Status { get; set; } = "Scheduled";
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public ICollection<UserFlightAssignment> Assignments { get; set; }
        public ICollection<Document> Document { get; set; }
    }
}
