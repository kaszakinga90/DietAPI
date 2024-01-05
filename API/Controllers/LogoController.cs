using Application.CQRS.Logos;
using Application.DTOs.LogoDTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    //[Authorize(Roles = "Admin, Dietetician")]
    public class LogoController : BaseApiController
    {
        public LogoController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost("logocreateorupdate")]
        public async Task<IActionResult> CreateLogo([FromForm] LogoPostDTO logoDto, [FromForm] IFormFile file)
        {
            var command = new CreateLogo.Command
            {
                LogoPostDTO = logoDto,
                File = file,
            };

            await _mediator.Send(command);

            return Ok();
        }

        [HttpGet("getlogo/{dieticianId}")]
        public async Task<ActionResult<LogoGetDTO>> GetLogo(int dieticianId)
        {
            var result = await _mediator.Send(new LogoDieticianDetails.Query { DieticianId = dieticianId });
            return HandleResult(result);
        }

        [HttpDelete("{dieticianId}")]
        public async Task<ActionResult<LogoPostDTO>> DeleteLogo(int dieticianId)
        {
            var command = new LogoDieticianDelete.Command { DieticianId = dieticianId };

            return HandleResult(await _mediator.Send(command));
        }
    }
}
