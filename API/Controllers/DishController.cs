using Application.CQRS.DieticiansBusinessesCards;
using Application.CQRS.Dishes;
using Application.DTOs.DieticianBusinessCardDTO;
using Application.DTOs.DishDTO;
using Application.FiltersExtensions.Dishes;
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
        public async Task<IActionResult> GetDish(int dieticianid)
        {
            var result = await _mediator.Send(new DishDetails.Query { Id = dieticianid });
            return HandleResult(result);
        }

        //pobiera dania dostępne dla dietetyka (czyli z bazy wspólnej, gdzie DieticianID == NULL oraz te utworzone przez tego dietetyka) + paginacja
        [HttpGet("allWithPagination/{dieticianId}")]
        public async Task<IActionResult> GetDishes(int dieticianId, [FromQuery] DishParams pagingParams)
        {
            var result = await _mediator.Send(new DishesListWithPagination.Query { DieteticianId = dieticianId, Params = pagingParams });
            return HandleResult(result);
        }

        //pobiera dania dostępne dla dietetyka (czyli z bazy wspólnej, gdzie DieticianID == NULL oraz te utworzone przez tego dietetyka) - bez paginacji
        [HttpGet("allNoPagination/{dieticianId}")]
        public async Task<IActionResult> GetDishesNoPagination(int dieticianId)
        {
            var result = await _mediator.Send(new DishesList.Query { DieteticianId = dieticianId });
            return HandleResult(result);
        }

        // TODO : poniżej nalezy dodać [FromForm]
        [HttpPost("create")]
        public async Task<IActionResult> CreateDish(DishPostDTO dishDto)
        {
            var command = new DishCreate.Command
            {
                DishPostDTO = dishDto,
                //File = file,
            };

            await _mediator.Send(command);

            return Ok();
        }

        [HttpGet("filters/{dishId}")]
        public async Task<ActionResult<DishFiltersDTO>> GetFilters(int dishId)
        {
            var result = await _mediator.Send(new DishFilterList.Query { DishId = dishId });
            return HandleResult(result);
        }
    }
}
