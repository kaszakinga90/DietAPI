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
        public async Task<IActionResult> GetFullListIngredients(int dieticianId, [FromQuery] IngredientsParams param)
        {
            var result = await _mediator.Send(new IngredientDieticianList.Query { DieticianId = dieticianId, Params = param });
            return HandlePagedResult(result);
        }

        [HttpGet("onlydietician/{dieticianId}")]
        public async Task<IActionResult> GetDieticianIngredients(int dieticianId, [FromQuery] IngredientsParams param)
        {
            var result = await _mediator.Send(new IngredientONLYDieticianList.Query { DieticianId = dieticianId, Params = param });
            return HandlePagedResult(result);
        }

        [HttpGet("list/{dieticianId}")]
        public async Task<IActionResult> GetIngredients(int dieticianId, [FromQuery] IngredientsParams param)
        {
            var result = await _mediator.Send(new IngredientList.Query { DieticianId = dieticianId, Params = param });
            return HandlePagedResult(result);
        }

        [HttpGet("{ingredientId}")]
        public async Task<IActionResult> GetIngredient(int ingredientId)
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

        // TODO : edycja
        // usuwanie - na podobnej zasadzie jak dish

        // TODO : usuwanie dish
        // - tam, gdzie jest pobierana lista dań do wyboru do diety to warunek isActive == true
        // - a przy szczegółach diety już bez warunku
    }
}