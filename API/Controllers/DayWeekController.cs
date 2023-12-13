using Application.CQRS.DayWeekDTOs;
using Application.CQRS.DayWeeks;
using Application.DTOs.DayWeekDTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class DayWeekController : BaseApiController
    {
        public DayWeekController(IMediator mediator) : base(mediator)
        {
        }
        [HttpGet]
        public async Task<ActionResult<List<DayWeekDTO>>> GetDaysWeek()
        {
            return await _mediator.Send(new DayWeekList.Query());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<DayWeekDTO>> GetDayWeek(int id)
        {
            return await _mediator.Send(new DayWeekDetails.Query { Id = id });
        }
    }
}
