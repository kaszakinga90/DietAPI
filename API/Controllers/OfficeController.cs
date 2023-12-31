using Application.CQRS.FoodCatalogs;
using Application.CQRS.Ingredients;
using Application.CQRS.Offices;
using Application.CQRS.Patients;
using Application.DTOs;
using Application.DTOs.AddressDTO;
using Application.DTOs.IngredientDTO;
using Application.DTOs.OfficeDTO;
using Application.DTOs.PatientDTO;
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
            return Ok(result); 
        }

        [HttpGet("getalldieticianoffices/{dieticianId}")]
        public async Task<IActionResult> GetOfficesList(int dieticianId)
        {
            var result = await _mediator.Send(new OfficeList.Query { DieteticianId = dieticianId });
            return HandleResult(result);
        }

        // TODO : zmiana nazwy metody
        [HttpGet("details/{officeId}")]
        public async Task<ActionResult<OfficeGetDTO>> GetIngredient(int officeId)
        {
            var result = await _mediator.Send(new OfficeDetails.Query { OfficeId = officeId });
            return HandleResult(result);
        }

        // TODO : zmiana nazwy metody
        [HttpPut("edit/{officeId}")]
        public async Task<IActionResult> EditPatientData(int officeId, OfficeEditDTO officeEdit)
        {
            var command = new OfficeEdit.Command
            {
                OfficeEdit = officeEdit,
            };
            command.OfficeEdit.Id = officeId;

            return HandleResult(await _mediator.Send(command));
        }
    }
}