using Application.CQRS.Units;
using Application.DTOs.UnitDTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class UnitController : BaseApiController
    {
        public UnitController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UnitGetDTO>> GetUnit(int id)
        {
            return await _mediator.Send(new UnitDetails.Query { Id = id });
        }

        [HttpGet("all")]
        public async Task<ActionResult<List<UnitGetDTO>>> GetUnits()
        {
            var result = await _mediator.Send(new UnitList.Query());
            return HandleResult(result);
        }
    }
}
