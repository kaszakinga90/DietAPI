using Application.Core;
using Application.CQRS.Diets;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class DietController : BaseApiController
    {
        [HttpGet("{dieticianId}")]
        public async Task<IActionResult> GetDiets( int dieticianId, [FromQuery] PagingParams pagingParams)
        {
            
            var result = await Mediator.Send(new DietList.Query { DieticianId=dieticianId, Params=pagingParams});
            return HandlePagedResult(result);
        }
        [HttpPost]
        public async Task<IActionResult> CreateDiet( DietDTO diet)
        {
            await Mediator.Send(new DietCreate.Command { DietDTO = diet });
            return Ok();
        }
    }
}
