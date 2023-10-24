using Application.CQRS.DayWeekDTOs;
using Application.CQRS.DayWeeks;
using Application.DTOs.DayWeekDTO;
using Microsoft.AspNetCore.Mvc;
using ModelsDB.Functionality;

namespace API.Controllers
{
    public class DayWeekController : BaseApiController
    {

        [HttpGet]
        public async Task<ActionResult<List<DayWeekDTO>>> GetDaysWeek()
        {
            return await Mediator.Send(new DayWeekList.Query());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<DayWeekDTO>> GetDayWeek(int id)
        {
            return await Mediator.Send(new DayWeekDetails.Query { Id = id });
        }
    }
}
