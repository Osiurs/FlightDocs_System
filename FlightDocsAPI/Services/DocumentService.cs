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

        public async Task<bool> UpdateDocumentAsync(int id, Document document)
    {
        var existingDocument = await GetDocumentByIdAsync(id);

        if (existingDocument == null)
        {
            return false; // Document không tồn tại
        }

        // Cập nhật các trường của document
        existingDocument.FlightID = document.FlightID;
        existingDocument.DocumentType = document.DocumentType;
        existingDocument.Content = document.Content;
        existingDocument.Status = document.Status;
        existingDocument.ModifiedAt = DateTime.Now;

        // Lưu thay đổi vào database
        _context.Document.Update(existingDocument);
        var updated = await _context.SaveChangesAsync();
        return updated > 0;
    }

        public async Task<Document> PatchDocumentAsync(Document document)
        {
            var existingDocument = await _context.Document.FindAsync(document.DocumentID);
            
            if (existingDocument == null)
            {
                throw new Exception("Document not found.");
            }

            // Cập nhật tất cả các thuộc tính đã thay đổi
            _context.Entry(existingDocument).CurrentValues.SetValues(document);
            existingDocument.ModifiedAt = DateTime.Now; // Cập nhật thời gian sửa đổi

            await _context.SaveChangesAsync();
            return existingDocument; // Trả về tài liệu đã được cập nhật
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
