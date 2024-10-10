using FlightDocsAPI.Models;
using FlightDocsAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;

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
        public async Task<IActionResult> UploadDocument([FromBody] Document model)
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
            var document = await _documentService.GetDocumentsByFlightIdAsync(flightId);
            return Ok(document);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDocument(int id, [FromBody] Document model)
        {
            if (id != model.DocumentID)
            {
                return BadRequest("Document ID mismatch.");
            }

            var updatedDocument = await _documentService.UpdateDocumentAsync(id, model);
            
            if (updatedDocument == null)
            {
                return NotFound("Document not found.");
            }

            return Ok(updatedDocument);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchDocument(int id, [FromBody] JsonPatchDocument<Document> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest("Invalid patch document.");
            }

            var document = await _documentService.GetDocumentByIdAsync(id);
            if (document == null)
            {
                return NotFound("Document not found.");
            }

            // Áp dụng các thay đổi từ patchDoc vào đối tượng document
            patchDoc.ApplyTo(document, (error) => ModelState.AddModelError("", error.ErrorMessage));

            // Kiểm tra xem có lỗi sau khi patch hay không
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Cập nhật lại thời gian sửa đổi
            document.ModifiedAt = DateTime.Now;

            await _documentService.PatchDocumentAsync(document); // Lưu các thay đổi vào database

            return Ok(document);
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
