using Application.CQRS.CountryStates;
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
        public async Task<IActionResult> GetCountryStates()
        {
            var result = await _mediator.Send(new CountryStateList.Query());
            return HandleResult(result);
        }

        // TODO : pozostałe metody - create, detail, update, delete - tylko superadmin
    }
}
