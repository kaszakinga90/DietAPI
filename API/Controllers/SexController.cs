using Microsoft.AspNetCore.Mvc;
using Application.DTOs.SexDTO;
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
        [HttpGet]
        [Route("allSexTypesFromView")]
        public async Task<ActionResult<List<SexGetDTO>>> GetSexesFromView()
        {
            var result = await _mediator.Send(new SexesList.Query());
            return HandleResult(result);
        }
    }
}
