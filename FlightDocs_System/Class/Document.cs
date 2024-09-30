public class Document
{
    public int DocumentID { get; set; }
    public int FlightID { get; set; }
    public string DocumentType { get; set; }
    public string Content { get; set; }
    public string Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }
    public Flight Flight { get; set; }
}
