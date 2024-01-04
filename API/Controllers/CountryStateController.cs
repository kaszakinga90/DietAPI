using Application.CQRS.CountryStates;
using Application.DTOs.CountryStateDTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class CountryStateController : BaseApiController
    {
        public CountryStateController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet("all")]
        public async Task<ActionResult<List<CountryStateGetDTO>>> GetContryStates()
        {
            var result = await _mediator.Send(new CountryStateList.Query());
            return HandleResult(result);
        }
    }
}
