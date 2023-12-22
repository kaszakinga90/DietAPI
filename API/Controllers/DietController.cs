using Application.Core;
using Application.CQRS.Diets;
using Application.CQRS.DietsForPatients;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class DietController : BaseApiController
    {
        public DietController(IMediator mediator) : base(mediator)
        {
        }

        // TODO : czy to ma być?
        [HttpGet("{dieticianId}")]
        public async Task<IActionResult> GetDiets(int dieticianId, [FromQuery] PagingParams pagingParams)
        {

            var result = await _mediator.Send(new DietList.Query { DieticianId = dieticianId, Params = pagingParams });
            return HandlePagedResult(result);
        }

        [HttpPost("adddiet")]
        public async Task<IActionResult> CreateDiet(DietDTO diet)
        {
            await _mediator.Send(new DietCreate.Command { DietDTO = diet });
            return Ok();
        }

        [HttpGet("dieticianDiets/{dieticianId}")]
        public async Task<IActionResult> GetDietsForDietician(int dieticianId, [FromQuery] PagingParams pagingParams)
        {
            var result = await _mediator.Send(new DietsForDieticianList.Query { DieticianId = dieticianId, Params = pagingParams });
            return HandlePagedResult(result);
        }

        [HttpGet("patientDiets/{patientId}")]
        public async Task<IActionResult> GetDietsForPatient(int patientId, [FromQuery] PagingParams pagingParams)
        {
            var result = await _mediator.Send(new DietsForPatientList.Query { PatientId = patientId, Params = pagingParams });
            return HandlePagedResult(result);
        }
    }
}
