using Application.CQRS.Messages;
using Application.CQRS.Patients;
using Application.DTOs.MessagesDTO;
using Application.DTOs.PatientDTO;
using Application.FiltersExtensions.PatientMessages;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Application.CQRS.Patients.MessagesFilters;

namespace API.Controllers
{
    public class PatientController : BaseApiController
    {
        public PatientController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetPatients()
        {
            var result = await _mediator.Send(new PatientList.Query());
            return HandlePagedResult(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPatient(int id)
        {
            var result = await _mediator.Send(new PatientDetails.Query { Id = id });
            return HandleResult(result);
        }

        //[Authorize(Roles = "SuperAdmin, Admin, Patient")]
        [HttpPut("{id}")]
        public async Task<IActionResult> EditPatient(int id, [FromForm] PatientEditDTO patientDTO, [FromForm] IFormFile file)
        {
            var command = new PatientEdit.Command
            {
                PatientEditDTO = patientDTO,
                File = file
            };
            command.PatientEditDTO.Id = id;

            var result = await _mediator.Send(command);
            if (result.IsSucces)
            {
                return Ok(new { data = result.Value, message = "Pomyœlnie zedytowano dane." });
            }
            return BadRequest(result.Error);
        }

        //[Authorize(Roles = "SuperAdmin, Admin, Patient")]
        [HttpPut("editdata/{patientId}")]
        public async Task<IActionResult> EditPatientData(int patientId, PatientEditDataDTO patient)
        {
            var command = new PatientEditData.Command
            {
                PatientEditDataDTO = patient,
            };
            command.PatientEditDataDTO.Id = patientId;

            var result = await _mediator.Send(command);
            if (result.IsSucces)
            {
                return Ok(new { data = result.Value, message = "Pomyœlnie zedytowano dane." });
            }
            return BadRequest(result.Error);
        }

        //[Authorize(Roles = "SuperAdmin, Admin, Patient")]
        [HttpGet("messages/{patientId}")]
        public async Task<IActionResult> GetMessagesForPatient(int patientId, [FromQuery] PatientMessagesParams param)
        {
            var result = await _mediator.Send(new PatientMessageList.Query 
            { 
                PatientId = patientId, 
                Params = param 
            });

            return HandlePagedResult(result);
        }

        [Authorize(Roles = "SuperAdmin, Admin, Dietetician, Patient")]
        [HttpGet("messagesnopagination/{patientId}")]
        public async Task<IActionResult> GetMessagesForPatientNoPagination(int patientId)
        {
            var result = await _mediator.Send(new PatientMessageNoPaginationList.Query { PatientId = patientId });
            return HandleResult(result);
        }

        [HttpGet("filters")]
        public async Task<IActionResult> GetFilters()
        {
            var result = await _mediator.Send(new PatientFilterList.Query());
            return HandleResult(result);
        }

        [HttpGet("filters/{patientId}")]
        public async Task<ActionResult<PatientMessagesFiltersDTO>> GetFiltersForPatientMessages(int patientId)
        {
            var result = await _mediator.Send(new FilterList.Query { PatientId = patientId });
            return HandleResult(result);
        }

        //[Authorize(Roles = "SuperAdmin, Admin, Dietetician, Patient")]
        [HttpPost("message/isread")]
        public async Task<IActionResult> MessageIsRead(MessageIsReadPostDTO messageIsRead)
        {
            var command = new MessageIsRead.Command
            {
                MessageIsReadPostDTO = messageIsRead
            };

            var result = await _mediator.Send(command);

            return Ok(result);
        }

        //[Authorize(Roles = "SuperAdmin, Admin, Patient, Dietetician")]
        [HttpPost("messageToDietician/{patientId}")]
        public async Task<IActionResult> MessageToDietetician(int patientId, MessageToDTO message)
        {
            var command = new MessageToDieteticianFromPatientCreate.Command
            {
                MessageDTO = message,
                PatientId = patientId
            };
            var result = await _mediator.Send(command);
            if (result.IsSucces)
            {
                return Ok(new { data = result.Value, message = "Pomyœlnie wys³ano wiadomoœæ." });
            }
            return BadRequest(result.Error);
        }

        [Authorize(Roles = "SuperAdmin, Admin, Patient")]
        [HttpPost("messageToAdmin/{patientId}")]
        public async Task<IActionResult> MessageToAdmin(int patientId, MessageToDTO message)
        {
            var command = new MessageToAdminFromPatientCreate.Command
            {
                MessageDTO = message,
                PatientId = patientId
            };
            var result = await _mediator.Send(command);
            if (result.IsSucces)
            {
                return Ok(new { data = result.Value, message = "Pomyœlnie wys³ano wiadomoœæ." });
            }
            return BadRequest(result.Error);
        }

        [HttpGet("allnopagination")]
        public async Task<IActionResult> GetCountryStates()
        {
            var result = await _mediator.Send(new PatientsListNoPagination.Query());
            return HandleResult(result);
        }
    }
}