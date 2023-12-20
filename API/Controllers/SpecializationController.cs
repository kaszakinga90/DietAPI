using Application.CQRS.Specializations;
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
        public async Task<IActionResult> CreateDiet(DieteticianSpecializationPostDTO ds)
        {
            var result=await _mediator.Send(new DieteticianSpecializationCreate.Command { DieteticianSpecializationPostDTOs = ds });
            return Ok(result.Value);
        }
    }
}
