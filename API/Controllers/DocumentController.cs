﻿using Application.CQRS.Printouts;
using Application.DTOs.PrintoutsDTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace API.Controllers
{
    // Kontroler zarządzający operacjami na dokumentach i szablonach wydruku.
    public class DocumentController : BaseApiController
    {
        public DocumentController(IMediator mediator) : base(mediator)
        {
        }

        /// <summary>
        /// Dodaje nowy szablon wydruku. Dostępne tylko dla użytkowników z rolą SuperAdmin.
        /// </summary>
        /// <param name="printout">Dane szablonu wydruku.</param>
        /// <returns>Wynik dodania szablonu.</returns>
        [HttpPost("addPrintout")]
        public async Task<IActionResult> AddPrintout([FromForm] ParameterizedPrintoutPostDTO printout)
        {
            var command = new PrintoutTemplateCreate.Command { Data = printout };
            var result = await _mediator.Send(command);
            if (result.IsSucces)
            {
                return Ok(new { data = result.Value, message = "Pomyślnie dodano szablon." });
            }
            return BadRequest(result.Error);
        }

        /// <summary>
        /// Pobiera listę wszystkich szablonów wydruku.
        /// </summary>
        /// <returns>Lista szablonów wydruku.</returns>
        [HttpGet("all")]
        public async Task<IActionResult> GetPrintouts()
        {
            var result = await _mediator.Send(new PrintoutList.Query());
            return HandleResult(result);
        }

        /// <summary>
        /// Generuje dokument na podstawie szablonu. Dostępne dla różnych ról użytkowników.
        /// </summary>
        /// <param name="printout">Dane potrzebne do wygenerowania dokumentu.</param>
        /// <returns>Wygenerowany dokument.</returns>
        [HttpPost("generatePrintoutDocument/byTemplate")]
        public async Task<IActionResult> GenerateDocument(PrintoutDocumentPostDTO printout)
        {
            Console.WriteLine($"Otrzymano zapytanie z PrintoutDocumentPostDTO: {JsonConvert.SerializeObject(printout)}");

            var result = await _mediator.Send(new PrintoutDocumentCreate.Command { Data = printout });

            string filePath = result.Value;

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound("Wygenerowany plik nie został znaleziony.");
            }

            byte[] fileBytes = await System.IO.File.ReadAllBytesAsync(filePath);

            var fileName = Path.GetFileName(filePath);

            return File(fileBytes, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", fileName);
        }

        /// <summary>
        /// Generuje dokument na podstawie szablonu dostarczonego przez użytkownika.
        /// </summary>
        /// <param name="printout">Dane i plik szablonu przesłany przez użytkownika.</param>
        /// <returns>Wygenerowany dokument.</returns>
        [HttpPost("generatePrintoutDocument/byUserTemplate")]
        public async Task<IActionResult> GenerateDocumentUploadFromUser([FromForm] PrintoutUploadByUserPostDTO printout)
        {
            var result = await _mediator.Send(new PrintoutByUserTemplateCreate.Command { Data = printout });

            return File(result.Value, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "filledTemplate.docx");
        }

        /// <summary>
        /// Usuwa szablon wydruku. Dostępne tylko dla użytkowników z rolą SuperAdmin.
        /// </summary>
        /// <param name="printoutId">Identyfikator szablonu do usunięcia.</param>
        /// <returns>Wynik usunięcia szablonu.</returns>
        [HttpDelete("delete/{printoutId}")]
        public async Task<IActionResult> RemovePrintoutTemplate(int printoutId)
        {
            var command = new PrintoutDelete.Command { PrintoutId = printoutId };
            var result = await _mediator.Send(command);
            if (result.IsSucces)
            {
                return Ok(new { data = result.Value, message = "Pomyślnie usunięto szablon." });
            }
            return BadRequest(result.Error);
        }
    }
}
