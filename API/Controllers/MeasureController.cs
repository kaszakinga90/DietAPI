using Application.CQRS.Measures;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class MeasureController : BaseApiController
    {
        public MeasureController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMeasure(int id)
        {
            var result = await _mediator.Send(new MeasureDetails.Query { Id = id });
            return HandleResult(result);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetMeasures()
        {
            var result = await _mediator.Send(new MeasureList.Query());
            return HandleResult(result);
        }
    }
}