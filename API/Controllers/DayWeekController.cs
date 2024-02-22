using Application.CQRS.DayWeeks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize(Roles = "SuperAdmin, Admin, Dietetician, Patient")]
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
    }
}