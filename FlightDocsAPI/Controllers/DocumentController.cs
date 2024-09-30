using FlightDocsAPI.Models;
using FlightDocsAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace FlightDocsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentService _documentService;

        public DocumentController(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadDocument([FromBody] DocumentModel model)
        {
            var document = await _documentService.UploadDocumentAsync(model);
            return CreatedAtAction(nameof(GetDocumentById), new { id = document.DocumentID }, document);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDocumentById(int id)
        {
            var document = await _documentService.GetDocumentByIdAsync(id);
            if (document == null) return NotFound();
            return Ok(document);
        }

        [HttpGet("flight/{flightId}")]
        public async Task<IActionResult> GetDocumentsByFlight(int flightId)
        {
            var documents = await _documentService.GetDocumentsByFlightIdAsync(flightId);
            return Ok(documents);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDocument(int id)
        {
            var result = await _documentService.DeleteDocumentAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
