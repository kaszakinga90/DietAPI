using Application.CQRS.Measures;
using Application.DTOs.MeasureDTO;
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
        public async Task<ActionResult<MeasureGetDTO>> GetMeasure(int id)
        {
            return await _mediator.Send(new MeasureDetails.Query { Id = id });
        }

        [HttpGet("all")]
        public async Task<ActionResult<List<MeasureGetDTO>>> GetMeasures()
        {
            var result = await _mediator.Send(new MeasureList.Query());
            return HandleResult(result);
        }
    }
}