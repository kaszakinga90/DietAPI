using Application.CQRS.DieticiansPatients;
using Application.DTOs.DieteticianPatientDTO;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class DieteticianPatientController : BaseApiController
    {
        [HttpGet("patient/{patientId}")]
        public async Task<ActionResult<List<DieteticianPatientDTO>>> GetDieteticianPatient(int patientId)
        {

            var result = await Mediator.Send(new FromPatientToDieteticianList.Query { PatientId = patientId });
            return HandleResult(result);
        }
        [HttpGet("dietician/{dieticianId}")]
        public async Task<ActionResult<List<DieteticianPatientDTO>>> GetPatientFromDietician(int dieticianId)
        {

            var result = await Mediator.Send(new FromDieticianToPatientList.Query { DieticianId = dieticianId });
            return HandleResult(result);
        }
    }
}
