using FlightDocsAPI.Data;

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

    public async Task<DocumentDto> UploadDocumentAsync(DocumentDto documentDto)
    {
        var document = new Document
        {
            FlightID = documentDto.FlightID,
            DocumentType = documentDto.DocumentType,
            Content = documentDto.Content,
            Status = documentDto.Status,
            CreatedAt = DateTime.Now,
            ModifiedAt = DateTime.Now
        };

        _context.Document.Add(document);
        await _context.SaveChangesAsync();

        return documentDto;
    }

    public async Task<DocumentDto> GetDocumentByIdAsync(int id)
    {
        var document = await _context.Document.FindAsync(id);
        if (document == null) return null;

        return new DocumentDto
        {
            FlightID = document.FlightID,
            DocumentType = document.DocumentType,
            Content = document.Content,
            Status = document.Status
        };
    }

    public async Task<IEnumerable<DocumentDto>> GetDocumentsByFlightIdAsync(int flightId)
    {
        return await _context.Document
            .Where(d => d.FlightID == flightId)
            .Select(d => new DocumentDto
            {
                FlightID = d.FlightID,
                DocumentType = d.DocumentType,
                Content = d.Content,
                Status = d.Status
            })
            .ToListAsync();
    }

    public async Task<bool> UpdateDocumentAsync(int id, DocumentDto documentDto)
    {
        var document = await _context.Document.FindAsync(id);
        if (document == null) return false;

        document.DocumentType = documentDto.DocumentType;
        document.Content = documentDto.Content;
        document.Status = documentDto.Status;
        document.ModifiedAt = DateTime.Now;

        _context.Document.Update(document);
        await _context.SaveChangesAsync();

        return true;
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
