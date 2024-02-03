using Application.CQRS.Dishes.DishToEdit.Edits;
using Application.CQRS.Dishes.DishToEdit.Gets;
using Application.DTOs.DishDetailsToEditDTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ModelsDB;
using Org.BouncyCastle.Asn1.Ocsp;

namespace API.Controllers
{
    public class DishEditController : BaseApiController
    {
        public DishEditController(IMediator mediator) : base(mediator)
        {
        }

        #region metody GET dla elementów składowych Dish

        [HttpGet("get/dish/{dishId}")]
        public async Task<IActionResult> GetDishBaseDetails(int dishId)
        {
            var result = await _mediator.Send(new DishBaseDetails.Query { DishId = dishId });
            return HandleResult(result);
        }

        [HttpGet("get/ingredient/{dishId}")]
        public async Task<IActionResult> GetDishIngredientsDetails(int dishId)
        {
            var result = await _mediator.Send(new DishIngredientsDetailsList.Query { DishId = dishId });
            return HandleResult(result);
        }

        [HttpGet("get/foodCatalog/{dishId}")]
        public async Task<IActionResult> GetDishFoodCatalogDetails(int dishId)
        {
            var result = await _mediator.Send(new DishFoodCatalogDetailsList.Query { DishId = dishId });
            return HandleResult(result);
        }

        [HttpGet("get/recipe/{dishId}")]
        public async Task<IActionResult> GetDishRecipeDetails(int dishId)
        {
            var result = await _mediator.Send(new DishRecipeDetails.Query { DishId = dishId });
            return HandleResult(result);
        }

        #endregion

        #region metody EDIT dla elementów składowych Dish
        [HttpPut("update/dish/{dishId}")]
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

        [HttpPut("update/ingredient/")]
        public async Task<IActionResult> EditDishIngredientsDetails( List<DishIngredientsDetailsGetEditDTO> dishIngredientsDetailsGetEditListDto)
        {
            var command = new UpdateDishIngredientsDetailsList.Command
            {
                DishId = dishIngredientsDetailsGetEditListDto.FirstOrDefault()?.DishId ?? default(int),
                DishIngredientsDetailsGetEditDto = dishIngredientsDetailsGetEditListDto
            };

            var result = await _mediator.Send(command);
            if (result.IsSucces)
            {
                return Ok(new { data = result.Value, message = "Pomyślnie zedytowano Dish Ingredients." });
            }
            return BadRequest(result.Error);
        }

        [HttpPut("update/foodCatalog/")]
        public async Task<IActionResult> EditDishFoodCatalogDetails( List<DishFoodCatalogsDetailsGetEditDTO> dishFoodCatalogsDetailsGetEditListDto)
        {
            var command = new UpdateDishFoodCatalogDetailsList.Command
            {
                DishId = dishFoodCatalogsDetailsGetEditListDto.FirstOrDefault()?. DishId??default(int),
                DishFoodCatalogsDetailsGetEditDto = dishFoodCatalogsDetailsGetEditListDto
            };

            var result = await _mediator.Send(command);
            if (result.IsSucces)
            {
                return Ok(new { data = result.Value, message = "Pomyślnie zedytowano Dish Food Catalogs." });
            }
            return BadRequest(result.Error);
        }

        [HttpPut("update/recipe/{dishId}")]
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