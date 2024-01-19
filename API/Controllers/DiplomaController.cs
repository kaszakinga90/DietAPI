using Application.CQRS.Diplomas;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class DiplomaController : BaseApiController
    {
        public DiplomaController(IMediator mediator) : base(mediator)
        {
        }
         
        [HttpGet("{dieticianId}")]
        public async Task<IActionResult> GetDiplomas(int dieticianId)
        {
            var result = await _mediator.Send(new DiplomasDieticianList.Query { DieticianId = dieticianId });
            return HandleResult(result);
        }

        // DONE : metoda do usuwania dyplomu (usuwanie całkowite przez remove)
        [HttpDelete("delete/{dieticianId}/{diplomaId}")]
        public async Task<IActionResult> DeleteDiploma(int dieticianId, int diplomaId)
        {
            var command = new DiplomaDelete.Command { DieticianId = dieticianId, DiplomaId = diplomaId };

            return HandleResult(await _mediator.Send(command));
        }
    }
}