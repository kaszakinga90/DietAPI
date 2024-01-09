using Application.Core;
using Application.CQRS.Ingredients;
using Application.DTOs.IngredientDTO;
using Application.FiltersExtensions.Ingredients;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class IngredientController : BaseApiController
    {
        public IngredientController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet("full/{dieticianId}")]
        public async Task<ActionResult<PagedList<IngredientGetDTO>>> GetFullListIngredients(int dieticianId, [FromQuery] IngredientsParams param)
        {
            var result = await _mediator.Send(new IngredientDieticianList.Query { DieticianId = dieticianId, Params = param });
            return HandlePagedResult(result);
        }

        [HttpGet("onlydietician/{dieticianId}")]
        public async Task<ActionResult<PagedList<IngredientGetDTO>>> GetDieticianIngredients(int dieticianId, [FromQuery] IngredientsParams param)
        {
            var result = await _mediator.Send(new IngredientONLYDieticianList.Query { DieticianId = dieticianId, Params = param });
            return HandlePagedResult(result);
        }

        [HttpGet("list/{dieticianId}")]
        public async Task<ActionResult<PagedList<IngredientGetDTO>>> GetIngredients(int dieticianId, [FromQuery] IngredientsParams param)
        {
            var result = await _mediator.Send(new IngredientList.Query { DieticianId = dieticianId, Params = param });
            return HandlePagedResult(result);
        }

        [HttpGet("{ingredientId}")]
        public async Task<ActionResult<IngredientGetDTO>> GetIngredient(int ingredientId)
        {
            var result = await _mediator.Send(new IngredientDetails.Query { IngredientId = ingredientId });
            return HandleResult(result);
        }

        [HttpGet("getallnopagination/{dieticianId}")]
        public async Task<IActionResult> GetAllIngredientsNoPagination(int dieticianId)
        {
            var result = await _mediator.Send(new IngredientsAllListNOpagination .Query { DieteticianId = dieticianId });
            return HandleResult(result);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateIngredient([FromForm] IngredientDTO IngredientDTO, [FromForm] IFormFile file)
        {
            var command = new IngredientCreate.Command
            {
                IngredientDTO = IngredientDTO,
                File = file,
            };

            return HandleResult(await _mediator.Send(command));
        }
    }
}