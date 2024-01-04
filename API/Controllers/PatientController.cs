using Application.Core;
using Application.CQRS.Patients;
using Application.DTOs.MessagesDTO;
using Application.DTOs.PatientDTO;
using Application.FiltersExtensions.PatientMessages;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ModelsDB;
using static Application.CQRS.Patients.MessagesFilters;

namespace API.Controllers
{
    public class PatientController : BaseApiController
    {
        public PatientController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        [Route("all")]
        public async Task<ActionResult<List<Patient>>> GetPatients()
        {
            return await _mediator.Send(new PatientList.Query());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PatientGetDTO>> GetPatient(int id)
        {
            return await _mediator.Send(new PatientDetails.Query { Id = id });
        }

        [HttpPost]
        public async Task<IActionResult> CreatePatient(Patient Patient)
        {
            await _mediator.Send(new PatientCreate.Command { Patient = Patient });
            return Ok();
        }

        //zmiana zdjêcia, bo zagnie¿d¿ony adres
        [HttpPut("{id}")]
        public async Task<IActionResult> EditPatient(int id, [FromForm] PatientDTO patientDTO, [FromForm] IFormFile file)
        {
            var command = new PatientEdit.Command
            {
                Patient = patientDTO,
                File = file
            };
            command.Patient.Id = id;

            return HandleResult(await _mediator.Send(command));
        }

        //pospolita zmiana danych
        [HttpPut("{patientId}/editdata")]
        public async Task<IActionResult> EditPatientData(int patientId, PatientEditDataDTO patient)
        {
            var command = new PatientEditData.Command
            {
                PatientEditData = patient,
            };
            command.PatientEditData.Id = patientId;

            return HandleResult(await _mediator.Send(command));
        }

        [HttpGet("{patientId}/messages")]
        public async Task<ActionResult<PagedList<MessageToDTO>>> GetMessagesForPatient(int patientId, [FromQuery] PatientMessagesParams param)
        {
            var result = await _mediator.Send(new PatientMessageList.Query { PatientId = patientId, Params = param });

            return HandlePagedResult(result);
        }

        [HttpGet("filters/{patientId}")]
        public async Task<ActionResult<PatientMessagesFiltersDTO>> GetFilters(int patientId)
        {
            var result = await _mediator.Send(new FilterList.Query { PatientId = patientId });
            return HandleResult(result);
        }

        [HttpPost("messageToDietician/{patientId}")]
        public async Task<IActionResult> MessageToDietetician(int patientId, MessageToDTO message)
        {
            await _mediator.Send(new MessageToDieteticianFromPatientCreate.Command { MessageDTO = message, PatientId = patientId });
            return Ok();
        }

        [HttpPost("{patientId}/messageToAdmin")]
        public async Task<IActionResult> MessageToAdmin(int patientId, MessageToDTO message)
        {
            await _mediator.Send(new MessageToAdminFromPatientCreate.Command { MessageDTO = message, PatientId = patientId });
            return Ok();
        }
    }
}
