using Application.CQRS.Dishes;
using Application.DTOs.DishDTO;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class DishController : BaseApiController
    {
        [HttpGet("{id}")]
        public async Task<ActionResult<DishGetDTO>> GetDish(int id)
        {
            return await Mediator.Send(new DishDetails.Query { Id = id });
        }

        //pobiera dania dostępne dla dietetyka (czyli z bazy wspólnej, gdzie DieticianID == NULL oraz te utworzone przez tego dietetyka)
        [HttpGet("all")]
        public async Task<IActionResult> GetDishes(int dieticianId)
        {
            var result = await Mediator.Send(new DishesList.Query { DieteticianId = dieticianId } );
            return HandleResult(result);
        }

        // poniżej nalezy dodać [FromForm]
        [HttpPost("create")]
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
