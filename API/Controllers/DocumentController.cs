﻿using Application.CQRS.Printouts;
using Application.DTOs.PrintoutsDTO;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace API.Controllers
{
    /// <summary>
    /// Kontroler zarządzający operacjami na wydrukach parametryzowanych
    /// </summary>
    public class DocumentController : BaseApiController
    {
        public DocumentController(IMediator mediator) : base(mediator)
        {
        }

        /// <summary>
        /// Dodanie nowego wydruku parametryzowanego
        /// </summary>
        /// <param name="printout">Dane zawarte w wydruku</param>
        /// <returns>Wynik dodania wydruku</returns>
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
        /// Pobiera listę wszystkich szablonów wydruków parametryzowanych
        /// </summary>
        /// <returns>Lista szablonów wydruków parametryzowanych</returns>
        [HttpGet("all")]
        public async Task<IActionResult> GetPrintouts()
        {
            var result = await _mediator.Send(new PrintoutList.Query());
            return HandleResult(result);
        }

        /// <summary>
        /// Generuje dokument na podstawie szablonu wydruku parametryzowanego
        /// </summary>
        /// <param name="printout">Dane potrzebne do wygenerowania pliku</param>
        /// <returns>Wygenerowany dokument</returns>
        [HttpPost("generatePrintoutDocument/byTemplate")]
        public async Task<IActionResult> GenerateDocument(PrintoutDocumentPostDTO printout)
        {
            var result = await _mediator.Send(new PrintoutDocumentCreate.Command { Data = printout });

            if (result.IsSucces)
            {
                return File(result.Value, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "generatedDocument.docx");
            }
            else
            {
                return BadRequest(result.Error);
            }
        }

        /// <summary>
        /// Generuje dokument na podstawie szablonu wydruku dostarczonego przez użytkownika
        /// </summary>
        /// <param name="printout">Dane i plik dokumentu(wydruku) przesłany przez użytkownika</param>
        /// <returns>Wygenerowany dokument</returns>
        [HttpPost("generatePrintoutDocument/byUserTemplate")]
        public async Task<IActionResult> GenerateDocumentUploadFromUser([FromForm] PrintoutUploadByUserPostDTO printout)
        {
            var result = await _mediator.Send(new PrintoutByUserTemplateCreate.Command { Data = printout });

            return File(result.Value, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "filledTemplate.docx");
        }

        /// <summary>
        /// Usuwa szablon wydruku parametryzowanego
        /// </summary>
        /// <param name="printoutId">Identyfikator szablonu do usunięcia</param>
        /// <returns>Wynik usunięcia pliku</returns>
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
