using Application.Core;
using Application.CQRS.Diets;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class DietController : BaseApiController
    {
        public DietController(IMediator mediator) : base(mediator)
        {
        }
        [HttpGet("{dieticianId}")]
        public async Task<IActionResult> GetDiets(int dieticianId, [FromQuery] PagingParams pagingParams)
        {

            var result = await _mediator.Send(new DietList.Query { DieticianId = dieticianId, Params = pagingParams });
            return HandlePagedResult(result);
        }
        [HttpPost("adddiet")]
        public async Task<IActionResult> CreateDiet(DietDTO diet)
        {
            await _mediator.Send(new DietCreate.Command { DietDTO = diet });
            return Ok();
        }
    }
}
