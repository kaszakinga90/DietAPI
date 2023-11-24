using Application.CQRS.Measures;
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
        public async Task<ActionResult<List<Measure>>> GetMeasures()
        {
            return await Mediator.Send(new MeasureList.Query());
        }
    }
}