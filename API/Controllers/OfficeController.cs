using Application.CQRS.Offices;
using Application.DTOs;
using Application.DTOs.OfficeDTO;
using Application.Functionality;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class OfficeController : BaseApiController
    {
        public OfficeController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost("addoffice")]
        public async Task<IActionResult> CreateOffice([FromBody] OfficeCreationDTO officeCreationDto)
        {
            var command = new OfficeCreate.Command
            {
                OfficePostDTO = officeCreationDto.OfficeDto,
                AddressPostDTO = officeCreationDto.AddressDto,
                DieticianId = officeCreationDto.DieticianId
            };
            var result = await _mediator.Send(command);

            if (result.IsSucces)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);
        }

        [HttpGet("getalldieticianoffices/{dieticianId}")]
        public async Task<IActionResult> GetOfficesList(int dieticianId)
        {
            var result = await _mediator.Send(new OfficeList.Query { DieteticianId = dieticianId });
            return HandleResult(result);
        }

        [HttpGet("details/{officeId}")]
        public async Task<IActionResult> GetOffice(int officeId)
        {
            var result = await _mediator.Send(new OfficeDetails.Query { OfficeId = officeId });
            return HandleResult(result);
        }

        [HttpPut("edit/{officeId}")]
        public async Task<IActionResult> EditOfficeData(int officeId, OfficeEditDTO officeEdit)
        {
            var command = new OfficeEdit.Command
            {
                OfficeEditDTO = officeEdit,
            };
            command.OfficeEditDTO.Id = officeId;

            return HandleResult(await _mediator.Send(command));
        }

        // TODO : usuwanie
    }
}