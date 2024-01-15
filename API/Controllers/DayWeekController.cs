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

        [HttpGet("all")]
        public async Task<IActionResult> GetDaysWeek()
        {
            var result = await _mediator.Send(new DayWeekList.Query());
            return HandleResult(result);
        }

        // TODO : poniższe metody do usunięcia
        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetDayWeek(int id)
        //{
        //    var result = await _mediator.Send(new DayWeekDetails.Query { Id = id });
        //    return HandleResult(result);
        //}

        //[HttpDelete("delete/{id}")]
        //public async Task<IActionResult> DeleteDayWeek(int id, DayWeekDeleteDTO dayWeek)
        //{
        //    var command = new DayWeekDelete.Command
        //    {
        //        Id = id,
        //        DayWeekDeleteDTO = dayWeek,
        //    };
        //    return HandleResult(await _mediator.Send(command));
        //}
    }
}