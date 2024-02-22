using Application.CQRS.Logos;
using Application.DTOs.LogoDTO;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    // TODO : sposób przedstawienia nagłówków autoryzacyjnych
    public class LogoController : BaseApiController
    {
        public LogoController(IMediator mediator) : base(mediator)
        {
        }

        [Authorize(Roles = "SuperAdmin, Admin, Dietetician")]
        [HttpPost("logocreateorupdate")]
        public async Task<IActionResult> CreateLogo([FromForm] LogoPostDTO logoDto, [FromForm] IFormFile file)
        {
            var command = new LogoCreate.Command
            {
                LogoPostDTO = logoDto,
                File = file,
            };
            var result = await _mediator.Send(command);
            if (result.IsSucces)
            {
                return Ok(new { data = result.Value, message = "Pomyślnie dodano logo." });
            }
            return BadRequest(result.Error);
        }

        [HttpGet("getlogo/{dieticianId}")]
        public async Task<IActionResult> GetLogo(int dieticianId)
        {
            var result = await _mediator.Send(new LogoDieticianDetails.Query { DieticianId = dieticianId });
            return HandleResult(result);
        }

        [Authorize(Roles = "SuperAdmin, Admin, Dietetician")]
        [HttpDelete("delete/{dieticianId}")]
        public async Task<IActionResult> DeleteLogo(int dieticianId)
        {
            var command = new LogoDieticianDelete.Command { DieticianId = dieticianId };

            var result = await _mediator.Send(command);
            if (result.IsSucces)
            {
                return Ok(new { data = result.Value, message = "Pomyślnie usunięto logo." });
            }
            return BadRequest(result.Error);
        }
    }
}