using FlightDocsAPI.Data;
using FlightDocsAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightDocsAPI.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly FlightDocsContext _context;

        public DocumentService(FlightDocsContext context)
        {
            _context = context;
        }

        public async Task<Document> UploadDocumentAsync(Document model)
        {
            var document = new Document
            {
                FlightID = model.FlightID,
                DocumentType = model.DocumentType,
                Content = model.Content,
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now
            };

            _context.Documents.Add(document);
            await _context.SaveChangesAsync();
            return document;
        }

        public async Task<Document> GetDocumentByIdAsync(int id)
        {
            return await _context.Documents.FindAsync(id);
        }

        public async Task<IEnumerable<Document>> GetDocumentsByFlightIdAsync(int flightId)
        {
            return await _context.Documents.Where(d => d.FlightID == flightId).ToListAsync();
        }

        public async Task<bool> DeleteDocumentAsync(int id)
        {
            var document = await _context.Documents.FindAsync(id);
            if (document == null) return false;

            _context.Documents.Remove(document);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
