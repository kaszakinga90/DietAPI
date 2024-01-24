﻿using Application.CQRS.Specializations;
using Application.DTOs.SpecializationDTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class SpecializationController : BaseApiController
    {
        public SpecializationController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetSpecializations()
        {
            var result = await _mediator.Send(new SpecializationsList.Query());
            return HandleResult(result);
        }

        [HttpGet("details/{dieticianId}")]
        public async Task<IActionResult> GetDieticianSpecializations(int dieticianId)
        {
            var result = await _mediator.Send(new DieteticianSpecializationList.Query { DieticianId = dieticianId });
            return HandleResult(result);
        }

        [HttpPost("create")]
        public async Task<IActionResult> AddDieticianSpecialization(DieteticianSpecializationPostDTO ds)
        {
            var result = await _mediator.Send(new DieteticianSpecializationCreate.Command { DieteticianSpecializationPostDTOs = ds });
            return Ok(result.Value);
        }

        // tylko dla admina
        [HttpPost("add")]
        public async Task<IActionResult> AddSpecialization(SpecializationPostDTO specializationPostDTO)
        {
            var result = await _mediator.Send(new SpecializationCreate.Command { SpecializationPostDTO = specializationPostDTO });
            //return Ok(result.Value);
            return HandleResult(result);
        }

        // DONE : edycja specjalizacji - metoda tylko dla admina
        [HttpPut("editdata/{specializationId}")]
        public async Task<IActionResult> EditSpecialization(int specializationId, SpecializationPostDTO specializationDTO)
        {
            var command = new SpecializationEdit.Command
            {
                SpecializationEditDTO = specializationDTO,
            };
            command.SpecializationEditDTO.Id = specializationId;

            return HandleResult(await _mediator.Send(command));
        }

        // DONE : usuwanie specjalizacji - realizowana przez admina (tylko specjalizacje, które nie mają powiązań)
        [HttpDelete("delete/{specializationId}")]
        public async Task<IActionResult> RemoveSpecializationByAdmin(int specializationId)
        {
            var command = new SpecializationDelete.Command { SpecializationId = specializationId };

            return HandleResult(await _mediator.Send(command));
        }

        // DONE : usuwanie specjalizacji - realizowana przez dietetyka
        [HttpDelete("deleteFromDietician/{dieticianId}/{dieticianSpecializationId}")]
        public async Task<IActionResult> DeleteDieticianSpecialization(int dieticianId, int dieticianSpecializationId)
        {
            var command = new DieticianSpecializationDelete.Command 
            { 
                DieticianId = dieticianId,
                SpecializationId = dieticianSpecializationId 
            };

            return HandleResult(await _mediator.Send(command));
        }
    }
}