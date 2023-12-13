using Application.CQRS.Dishes;
using Application.DTOs.DishDTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class DishController : BaseApiController
    {

        public DishController(IMediator mediator) : base(mediator)
        {
        }
        
        [HttpGet("dieticianDish/{dieticianid}")]
        public async Task<ActionResult<DishGetDTO>> GetDish(int dieticianid)
        {
            return await _mediator.Send(new DishDetails.Query { Id = dieticianid });
        }

        //pobiera dania dostępne dla dietetyka (czyli z bazy wspólnej, gdzie DieticianID == NULL oraz te utworzone przez tego dietetyka)
        [HttpGet("all/{dieticianId}")]
        public async Task<IActionResult> GetDishes(int dieticianId)
        {
            var result = await _mediator.Send(new DishesList.Query { DieteticianId = dieticianId });
            return HandleResult(result);
        }


        // TODO : poniżej nalezy dodać [FromForm]
        [HttpPost("create")]
        public async Task<IActionResult> CreateDish( DishPostDTO dishDto)
        {
            var command = new DishCreate.Command
            {
                DishPostDTO = dishDto,
                //File = file,
            };

            await _mediator.Send(command);

            return Ok();
        }
    }
}
