﻿using Application.Core;
using Application.CQRS.Dieticians;
using Application.CQRS.Diplomas;
using Application.DTOs.DieticianDTO;
using Application.DTOs.DiplomaDTO;
using Application.DTOs.MessagesDTO;
using Application.FiltersExtensions.DieticianMessages;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ModelsDB;
using static Application.CQRS.Dieticians.MessagesFilters;

namespace API.Controllers
{
    public class DieticianController : BaseApiController
    {

        public DieticianController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet("all")]
        public async Task<ActionResult<List<Dietician>>> GetDieticians()
        {
            return await _mediator.Send(new DieticianList.Query());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDietician(int id)
        {
            var result = await _mediator.Send(new DieticianDetails.Query { Id = id } );
            return HandleResult(result);
        }

        [HttpPut("{dieticianId}")]
        public async Task<IActionResult> EditDietician(int dieticianId, [FromForm] DieticianEditDTO dieticianDto, [FromForm] IFormFile file)
        {
            var command = new DieticianEdit.Command
            {
                DieticianEditDTO = dieticianDto,
                File = file
            };
            command.DieticianEditDTO.Id = dieticianId;


            return HandleResult(await _mediator.Send(command));
        }

        [HttpPut("editdata/{dieticianId}")]
        public async Task<IActionResult> EditDieticianData(int dieticianId, DieticianEditDataDTO dietician)
        {
            var command = new DieticianEditData.Command
            {
                DieticianEditData = dietician,
            };
            command.DieticianEditData.Id = dieticianId;

            return HandleResult(await _mediator.Send(command));
        }

        [HttpGet("messages/{dieticianId}")]
        public async Task<ActionResult<PagedList<MessageToDTO>>> GetMessagesForDietician(int dieticianId, [FromQuery] DieticianMessagesParams param)
        {
            var result = await _mediator.Send(new DieticianMessageList.Query { DieticianId = dieticianId, Params = param });

            return HandlePagedResult(result);
        }

        [HttpGet("filters/{dieticianId}")]
        public async Task<ActionResult<DieticianMessagesFiltersDTO>> GetFilters(int dieticianId)
        {
            var result = await _mediator.Send(new FilterList.Query { DieticianId = dieticianId });
            return HandleResult(result);
        }

        [HttpPost("messageToAdmin/{dieticianId}")]
        public async Task<IActionResult> MessageToAdmin(int dieticianId, MessageToDTO message)
        {
            var command = new MessageToAdminFromDieticianCreate.Command
            {
                MessageDTO = message,
                DieticianId = dieticianId
            };

            return HandleResult(await _mediator.Send(command));
        }

        [HttpPost("messageToPatient/{dieticianId}")]
        public async Task<IActionResult> MessageToPatient(int dieticianId, MessageToDTO message)
        {
            var command = new MessageToPatientFromDieticianCreate.Command
            {
                MessageDTO = message,
                DieticianId = dieticianId
            };

            return HandleResult(await _mediator.Send(command));
        }

        [HttpPost("diploma")]
        public async Task<IActionResult> CreateDiploma([FromForm] DiplomaPostDTO diplomaDto, [FromForm] IFormFile file)
        {
            var command = new DiplomaCreate.Command
            {
                DiplomaPostDTO = diplomaDto,
                File = file,
            };

            await _mediator.Send(command);

            return Ok();
        }
    }
}