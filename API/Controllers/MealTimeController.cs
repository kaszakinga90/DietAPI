using Application.CQRS.MealTimes;
using Microsoft.AspNetCore.Mvc;
using ModelsDB;
using ModelsDB.Functionality;

namespace API.Controllers
{
    public class MealTimeController : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<List<MealTime>>> GetMealTimes()
        {
            return await Mediator.Send(new MealTimeList.Query());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<MealTime>> GetMealTime(int id)
        {
            return await Mediator.Send(new MealTimeDetails.Query { Id = id });
        }
    }
}
