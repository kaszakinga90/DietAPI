using Microsoft.AspNetCore.Mvc;
using Application.CQRS.Sexes;
using MediatR;

namespace API.Controllers
{
    public class SexController : BaseApiController
    {
        public SexController(IMediator mediator) : base(mediator)
        {
        }

        // IMPORTANT : FROM SQL - pobieranie danych z widoku
        [HttpGet("allSexTypesFromView")]
        public async Task<IActionResult> GetSexesFromView()
        {
            var result = await _mediator.Send(new SexesList.Query());
            return HandleResult(result);
        }
    }
}