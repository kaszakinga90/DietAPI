using Application.DayWeeks;
using Application.Examples;
using Microsoft.AspNetCore.Mvc;
using ModelsDB.Functionality;

namespace API.Controllers
{
    public class DayWeekController : BaseApiController
    {

        [HttpGet]
        public async Task<ActionResult<List<DayWeek>>> GetDaysWeek()
        {
            return await Mediator.Send(new DayWeekList.Query());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<DayWeek>> GetDayWeek(int id)
        {
            return await Mediator.Send(new DayWeekDetails.Query { Id = id });
        }
    }
}
