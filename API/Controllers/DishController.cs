using Application.CQRS.Dishes;
using Application.DTOs.DishDTO;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class DishController : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<List<DishGetDTO>>> GetDishes()
        {

            var result = await Mediator.Send(new DishesList.Query());
            return HandleResult(result);
        }

        [HttpPost("dish")]
        public async Task<IActionResult> CreateDish(DishPostDTO dishDto)
        {
            var command = new DishCreate.Command
            {
                DishPostDTO = dishDto,
                //File = file,
            };

            await Mediator.Send(command);

            return Ok();
        }
    }
}
