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
        [HttpGet("{dieticianId}/{patientId}")]
        public async Task<ActionResult<PatientCard>> GetPatientCardSP(int patientId, int dieticianId)
        {
            await _mediator.Send(new PatientCardDetails.Query { PatientId = patientId, DieticianId = dieticianId });
            return Ok();
        }

        // IMPORTANT : FROM SQL - tworzenie obiektu PatientCard za pomocą stored procedures
        [HttpPost("create")]
        public async Task<ActionResult<PatientCard>> CreatePatientCardSP(PatientCardPostDTO pc, int patientId, int dieticianId, int sexId)
        {
            await _mediator.Send(new PatientCardCreate.Command { PatientCardPostDTO = pc, PatientId = patientId, DieticianId = dieticianId, SexId = sexId });
            return Ok();
        }

        // TODO : lista wszystkich kart pacjentów u danego dietetyka + filtry i paginacja (u dietetyka, filtr po nazwie pacjenta)
        // edycja karty pacjenta
    }
}



// TODO - delete do uzupełnienia do survey i testresult