using Application.CQRS.MealTimes;
using Microsoft.AspNetCore.Mvc;
using ModelsDB;
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
    }
}
