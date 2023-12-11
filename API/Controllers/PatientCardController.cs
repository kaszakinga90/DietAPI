using Application.CQRS.PatientCards;
using Application.DTOs.PatientCardDTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ModelsDB;

namespace API.Controllers
{
    public class PatientCardController : BaseApiController
    {
        public PatientCardController(IMediator mediator) : base(mediator)
        {
        }
        // IMPORTANT : FROM SQL
        [HttpPost("create")]
        public async Task<ActionResult<PatientCard>> CreatePatientCardCQRS(PatientCardPostDTO pc, int patientId, int dieticianId, int sexId)
        {
            await _mediator.Send(new PatientCardCreate.Command { PatientCardPostDTO = pc, PatientId = patientId, DieticianId = dieticianId, SexId = sexId });
            return Ok();
        }
    }
}
