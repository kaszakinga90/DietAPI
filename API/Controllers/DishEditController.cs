using Application.CQRS.Dishes.DishToEdit.Edits;
using Application.CQRS.Dishes.DishToEdit.Gets;
using Application.DTOs.DishDetailsToEditDTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class DishEditController : BaseApiController
    {
        public DishEditController(IMediator mediator) : base(mediator)
        {
        }

        #region metody GET dla elementów składowych Dish

        [HttpGet("dish/{dishId}")]
        public async Task<IActionResult> GetDishBaseDetails(int dishId)
        {
            var result = await _mediator.Send(new DishBaseDetails.Query { DishId = dishId });
            return HandleResult(result);
        }

        [HttpGet("ingredient/{dishId}")]
        public async Task<IActionResult> GetDishIngredientsDetails(int dishId)
        {
            var result = await _mediator.Send(new DishIngredientsDetailsList.Query { DishId = dishId });
            return HandleResult(result);
        }

        [HttpGet("foodCatalog/{dishId}")]
        public async Task<IActionResult> GetDishFoodCatalogDetails(int dishId)
        {
            var result = await _mediator.Send(new DishFoodCatalogDetailsList.Query { DishId = dishId });
            return HandleResult(result);
        }

        [HttpGet("recipe/{dishId}")]
        public async Task<IActionResult> GetDishRecipeDetails(int dishId)
        {
            var result = await _mediator.Send(new DishRecipeDetails.Query { DishId = dishId });
            return HandleResult(result);
        }

        #endregion

        #region metody EDIT dla elementów składowych Dish
        [HttpPut("dish/{dishId}")]
        public async Task<IActionResult> EditDishBaseDetails(int dishId, DishDetailsGetEditDTO dishDetailsGetEditDto)
        {
            var command = new UpdateDishBaseDetails.Command { 
                DishId = dishId,
                DishDetailsGetEditDto = dishDetailsGetEditDto
            };

            var result = await _mediator.Send(command);
            if (result.IsSucces)
            {
                return Ok(new { data = result.Value, message = "Pomyślnie zedytowano Dish Details." });
            }
            return BadRequest(result.Error);
        }

        [HttpPut("ingredient/{dishId}")]
        public async Task<IActionResult> EditDishIngredientsDetails(int dishId, List<DishIngredientsDetailsGetEditDTO> dishIngredientsDetailsGetEditListDto)
        {
            var command = new UpdateDishIngredientsDetailsList.Command
            {
                DishId = dishId,
                DishIngredientsDetailsGetEditDto = dishIngredientsDetailsGetEditListDto
            };

            var result = await _mediator.Send(command);
            if (result.IsSucces)
            {
                return Ok(new { data = result.Value, message = "Pomyślnie zedytowano Dish Ingredients." });
            }
            return BadRequest(result.Error);
        }

        [HttpPut("foodCatalog/{dishId}")]
        public async Task<IActionResult> EditDishFoodCatalogDetails(int dishId, List<DishFoodCatalogsDetailsGetEditDTO> dishFoodCatalogsDetailsGetEditListDto)
        {
            var command = new UpdateDishFoodCatalogDetailsList.Command
            {
                DishId = dishId,
                DishFoodCatalogsDetailsGetEditDto = dishFoodCatalogsDetailsGetEditListDto
            };

            var result = await _mediator.Send(command);
            if (result.IsSucces)
            {
                return Ok(new { data = result.Value, message = "Pomyślnie zedytowano Dish Food Catalogs." });
            }
            return BadRequest(result.Error);
        }

        [HttpPut("recipe/{dishId}")]
        public async Task<IActionResult> EditDishRecipeDetails(int dishId, DishRecipeDetailsGetEditDTO dishRecipeDetailsGetEditDto)
        {
            var command = new UpdateDishRecipeDetails.Command
            {
                DishId = dishId,
                DishRecipeDetailsGetEditDto = dishRecipeDetailsGetEditDto
            };

            var result = await _mediator.Send(command);
            if (result.IsSucces)
            {
                return Ok(new { data = result.Value, message = "Pomyślnie zedytowano Dish Recipe Steps." });
            }
            return BadRequest(result.Error);
        }
        #endregion
    }
}