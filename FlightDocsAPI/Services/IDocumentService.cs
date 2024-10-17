

namespace FlightDocsAPI.Services
{
    public interface IDocumentService
    {
        Task<DocumentDto> UploadDocumentAsync(DocumentDto documentDto);
        Task<DocumentDto> GetDocumentByIdAsync(int id);
        Task<IEnumerable<DocumentDto>> GetDocumentsByFlightIdAsync(int flightId);
        Task<bool> UpdateDocumentAsync(int id, DocumentDto documentDto);
        Task<bool> DeleteDocumentAsync(int id);
    }

}
