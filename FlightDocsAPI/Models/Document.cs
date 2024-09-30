namespace FlightDocsAPI.Models
{
    public class Document
    {
        public int DocumentID { get; set; }
        public int FlightID { get; set; }
        public string DocumentType { get; set; }
        public string Content { get; set; }
        public string Status { get; set; } = "Pending";
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime ModifiedAt { get; set; } = DateTime.Now;

        public Flight Flight { get; set; }
    }
}
