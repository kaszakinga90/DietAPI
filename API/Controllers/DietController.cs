using Application.CQRS.Diets;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class DietController : BaseApiController
    {
        [HttpPost]
        public async Task<IActionResult> CreateDiet(DietDTO diet)
        {
            await Mediator.Send(new DietCreate.Command { DietDTO = diet });
            return Ok();
        }
    }
}
