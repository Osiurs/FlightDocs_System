using FlightDocsAPI.Models;

namespace FlightDocsAPI.Services
{
    public interface IDocumentService
    {
        Task<Document> UploadDocumentAsync(Document model);
        Task<Document> GetDocumentByIdAsync(int id);
        Task<IEnumerable<Document>> GetDocumentsByFlightIdAsync(int flightId);
        Task<bool> DeleteDocumentAsync(int id);
    }
}
