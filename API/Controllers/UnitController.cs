using Application.CQRS.Units;
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
        public async Task<IActionResult> GetUnit(int id)
        {
            var result = await _mediator.Send(new UnitDetails.Query { Id = id });
            return HandleResult(result);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetUnits()
        {
            var result = await _mediator.Send(new UnitList.Query());
            return HandleResult(result);
        }
    }
}