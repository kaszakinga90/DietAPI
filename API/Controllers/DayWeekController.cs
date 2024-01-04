using Application.CQRS.CategoryOfDiets;
using Application.CQRS.DayWeekDTOs;
using Application.CQRS.DayWeeks;
using Application.DTOs.CategoryOfDietDTO;
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

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteDayWeek(int id, DayWeekDeleteDTO dayWeek)
        {
            var command = new DayWeekDelete.Command
            {
                Id = id,
                DayWeekDeleteDTO = dayWeek,
            };
            return HandleResult(await _mediator.Send(command));
        }
    }
}
