using Application.CQRS.Printouts;
using Application.CQRS.Specializations;
using Application.CQRS.UserRoles;
using Application.DTOs.PrintoutsDTO;
using Application.DTOs.UsersDTO.UserRoleDTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Xceed.Words.NET;

namespace API.Controllers
{
    public class DocumentController : BaseApiController
    {
        public DocumentController(IMediator mediator) : base(mediator)
        {
        }

        // metoda tylko dla superadmina
        // dodawanie przez admina szablonu wydruku parametr.
        [HttpPost("addPrintout")]
        public async Task<IActionResult> AddPrintout([FromForm] ParameterizedPrintoutPostDTO printout)
        {
            var query = await _mediator.Send(new PrintoutTemplateCreate.Command { Data = printout });
            return HandleResult(query);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetPrintouts()
        {
            var result = await _mediator.Send(new PrintoutList.Query());
            return HandleResult(result);
        }

        // obróbka pliku word na podstawie dostarczonego przez nas szablonu.
        //[HttpPost("generatePrintoutDocument/byTemplate")]
        //public async Task<IActionResult> GenerateDocument([FromForm] PrintoutDocumentPostDTO printout)
        //{
        //    var query = await _mediator.Send(new PrintoutDocumentCreate.Command { Data = printout });
        //    return HandleResult(query);
        //}



        // obróbka pliku word na podstawie dostarczonego przez nas szablonu.
        [HttpPost("generatePrintoutDocument/byTemplate")]
        public async Task<IActionResult> GenerateDocument(PrintoutDocumentPostDTO printout)
        {
            Console.WriteLine($"Received request with PrintoutDocumentPostDTO: {JsonConvert.SerializeObject(printout)}");

            var result = await _mediator.Send(new PrintoutDocumentCreate.Command { Data = printout });

            //if (!result.IsSuccess)
            //{
            //    return BadRequest(result.Error);
            //}

            string filePath = result.Value;

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound("Wygenerowany plik nie został znaleziony.");
            }

            byte[] fileBytes = await System.IO.File.ReadAllBytesAsync(filePath);

            var fileName = Path.GetFileName(filePath);

            return File(fileBytes, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", fileName);
        }

        // obróbka pliku word na podstawie dostarczonego przez użytkownika pliku .
        [HttpPost("generatePrintoutDocument/byUserTemplate")]
        public async Task<IActionResult> GenerateDocumentUploadFromUser([FromForm] PrintoutUploadByUserPostDTO printout)
        {
            var result = await _mediator.Send(new PrintoutByUserTemplateCreate.Command { Data = printout });

            return File(result.Value, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "filledTemplate.docx");
        }

        // TODO : usuwanie szablonu
    }
}