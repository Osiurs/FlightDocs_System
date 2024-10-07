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

            _context.Document.Add(document);
            await _context.SaveChangesAsync();
            return document;
        }

        public async Task<Document> GetDocumentByIdAsync(int id)
        {
            return await _context.Document.FindAsync(id);
        }

        public async Task<IEnumerable<Document>> GetDocumentsByFlightIdAsync(int flightId)
        {
            return await _context.Document.Where(d => d.FlightID == flightId).ToListAsync();
        }

        public async Task<Document> UpdateDocumentAsync(int id, Document updatedDocument)
        {
            var existingDocument = await _context.Document.FindAsync(id);
            if (existingDocument == null) return null;

            // Cập nhật các trường cần thiết
            existingDocument.DocumentType = updatedDocument.DocumentType;
            existingDocument.Content = updatedDocument.Content;
            existingDocument.Status = updatedDocument.Status;
            existingDocument.ModifiedAt = DateTime.Now; // Cập nhật thời gian chỉnh sửa

            _context.Document.Update(existingDocument);
            await _context.SaveChangesAsync();

            return existingDocument;
        }


        public async Task<bool> DeleteDocumentAsync(int id)
        {
            var document = await _context.Document.FindAsync(id);
            if (document == null) return false;

            _context.Document.Remove(document);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
