using Application.CQRS.Dieticians;
using Application.CQRS.Diplomas;
using Application.CQRS.Messages;
using Application.DTOs.DieticianDTO;
using Application.DTOs.DiplomaDTO;
using Application.DTOs.MessagesDTO;
using Application.FiltersExtensions.DieticianMessages;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Application.CQRS.Dieticians.MessagesFilters;

namespace API.Controllers
{
    public class DieticianController : BaseApiController
    {

        public DieticianController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetDieticians()
        {
            var result = await _mediator.Send(new DieticianList.Query());
            return HandlePagedResult(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDietician(int id)
        {
            var result = await _mediator.Send(new DieticianDetails.Query { Id = id } );
            return HandleResult(result);
        }

        [Authorize(Roles = "SuperAdmin, Admin, Dietetician")]
        [HttpPut("{dieticianId}")]
        public async Task<IActionResult> EditDietician(int dieticianId, [FromForm] DieticianEditDTO dieticianDto, [FromForm] IFormFile file)
        {
            var command = new DieticianEdit.Command
            {
                DieticianEditDTO = dieticianDto,
                File = file
            };
            command.DieticianEditDTO.Id = dieticianId;

            var result = await _mediator.Send(command);
            if (result.IsSucces)
            {
                return Ok(new { data = result.Value, message = "Pomyślnie zedytowano dietetyka." });
            }
            return BadRequest(result.Error);
        }

        [Authorize(Roles = "SuperAdmin, Admin, Dietetician")]
        [HttpPut("editdata/{dieticianId}")]
        public async Task<IActionResult> EditDieticianData(int dieticianId, DieticianEditDataDTO dietician)
        {
            var command = new DieticianEditData.Command
            {
                DieticianEditData = dietician,
            };
            command.DieticianEditData.Id = dieticianId;

            var result = await _mediator.Send(command);
            if (result.IsSucces)
            {
                return Ok(new { data = result.Value, message = "Pomyślnie zedytowano dietetyka." });
            }
            return BadRequest(result.Error);
        }

        [Authorize(Roles = "SuperAdmin, Admin, Dietetician")]
        [HttpGet("messages/{dieticianId}")]
        public async Task<IActionResult> GetMessagesForDietician(int dieticianId, [FromQuery] DieticianMessagesParams param)
        {
            var result = await _mediator.Send(new DieticianMessageList.Query { 
                DieticianId = dieticianId, 
                Params = param 
            });

            return HandlePagedResult(result);
        }

        [HttpGet("filters")]
        public async Task<IActionResult> GetFilters()
        {
            var result = await _mediator.Send(new DieticiansFilterList.Query());
            return HandleResult(result);
        }

        [HttpGet("filters/{dieticianId}")]
        public async Task<IActionResult> GetFiltersForDieticianMessages(int dieticianId)
        {
            var result = await _mediator.Send(new FilterList.Query { DieticianId = dieticianId });
            return HandleResult(result);
        }

        [Authorize(Roles = "SuperAdmin, Admin, Dietetician")]
        [HttpPost("messageToAdmin/{dieticianId}")]
        public async Task<IActionResult> MessageToAdmin(int dieticianId, MessageToDTO message)
        {
            var command = new MessageToAdminFromDieticianCreate.Command
            {
                MessageDTO = message,
                DieticianId = dieticianId
            };
            var result = await _mediator.Send(command);

            if (result.IsSucces)
            {
                return Ok(new { data = result.Value, message = "Wiadomość została wysłana." });
            }
            return BadRequest(result.Error);
        }

        [Authorize(Roles = "SuperAdmin, Admin, Dietetician")]
        [HttpPost("messageToPatient/{dieticianId}")]
        public async Task<IActionResult> MessageToPatient(int dieticianId, MessageToDTO message)
        {
            var command = new MessageToPatientFromDieticianCreate.Command
            {
                MessageDTO = message,
                DieticianId = dieticianId
            };
            var result = await _mediator.Send(command);
            if (result.IsSucces)
            {
                return Ok(new { data = result.Value, message = "Wiadomość została wysłana." });
            }
            return BadRequest(result.Error);
        }

        [Authorize(Roles = "SuperAdmin, Admin, Dietetician")]
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

        [Authorize(Roles = "SuperAdmin, Admin, Dietetician")]
        [HttpPost("diploma")]
        public async Task<IActionResult> CreateDiploma([FromForm] DiplomaPostDTO diplomaDto, [FromForm] IFormFile file)
        {
            var command = new DiplomaCreate.Command
            {
                DiplomaPostDTO = diplomaDto,
                File = file,
            };
            var result = await _mediator.Send(command);
            if (result.IsSucces)
            {
                return Ok(new { data = result.Value, message = "Pomyślnie dodano dyplom." });
            }
            return BadRequest(result.Error);
        }

        [HttpGet("allnopagination")]
        public async Task<IActionResult> GetCountryStates()
        {
            var result = await _mediator.Send(new DieticianListNoPagination.Query());
            return HandleResult(result);
        }
    }
}