using Application.CQRS.DieticiansPatients;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class DieteticianPatientController : BaseApiController
    {
        public DieteticianPatientController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet("patient/{patientId}")]
        public async Task<IActionResult> GetDieteticianPatient(int patientId)
        {
            var result = await _mediator.Send(new FromPatientToDieteticianList.Query { PatientId = patientId });
            return HandleResult(result);
        }

        [HttpGet("dietician/{dieticianId}")]
        public async Task<IActionResult> GetPatientFromDietician(int dieticianId)
        {
            var result = await _mediator.Send(new FromDieticianToPatientList.Query { DieticianId = dieticianId });
            return HandleResult(result);
        }
    }
}