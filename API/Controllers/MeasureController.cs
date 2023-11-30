using Application.CQRS.Measures;
using Application.DTOs.MeasureDTO;
using Microsoft.AspNetCore.Mvc;
using ModelsDB;

namespace API.Controllers
{
    public class MeasureController : BaseApiController
    {
        [HttpGet("{id}")]
        public async Task<ActionResult<Measure>> GetMeasure(int id)
        {
            return await Mediator.Send(new MeasureDetails.Query { Id = id });
        }

        [HttpGet("all")]
        public async Task<ActionResult<List<MeasureGetDTO>>> GetMeasures()
        {
            var result = await Mediator.Send(new MeasureList.Query());
            return HandleResult(result);
        }
    }
}