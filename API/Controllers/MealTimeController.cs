using Application.CQRS.MealTimes;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ModelsDB.Functionality;

namespace API.Controllers
{
    public class MealTimeController : BaseApiController
    {
        public MealTimeController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        public async Task<ActionResult<List<MealTimeToXYAxis>>> GetMealTimes()
        {
            return await _mediator.Send(new MealTimeList.Query());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MealTimeToXYAxis>> GetMealTime(int id)
        {
            return await _mediator.Send(new MealTimeDetails.Query { Id = id });
        }

        [HttpPost]
        public async Task<IActionResult> CreateMealTime(MealTimeToXYAxis MealTime)
        {
            await _mediator.Send(new MealTimeCreate.Command { MealTime = MealTime });
            return Ok();
        }
    }
}
