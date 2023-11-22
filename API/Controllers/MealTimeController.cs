using Application.CQRS.MealTimes;
using Microsoft.AspNetCore.Mvc;
using ModelsDB.Functionality;

namespace API.Controllers
{
    public class MealTimeController : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<List<MealTimeToXYAxis>>> GetMealTimes()
        {
            return await Mediator.Send(new MealTimeList.Query());
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<MealTimeToXYAxis>> GetMealTime(int id)
        {
            return await Mediator.Send(new MealTimeDetails.Query { Id = id });
        }

        [HttpPost]
        public async Task<IActionResult> CreateMealTime(MealTimeToXYAxis MealTime)
        {
            await Mediator.Send(new MealTimeCreate.Command { MealTime = MealTime });
            return Ok();
        }
    }
}
