﻿using Application.CQRS.Dishes;
using Application.DTOs.DishDTO;
using Application.FiltersExtensions.Dishes;
using Application.FiltersExtensions.DishesFoodCatalog;
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
        [HttpGet("allfoodcatalogdishwithpagination/{foodCatalogId}")]
        public async Task<IActionResult> GetDishesFoodCatalogs(int foodCatalogId, [FromQuery] DishesFoodCatalogParams pagingParams)
        {
            var result = await _mediator.Send(new DishFoodCatalogList.Query { FoodCatalogId = foodCatalogId, Params = pagingParams });
            return HandleResult(result);
        }

        //pobiera dania dostępne dla dietetyka (czyli z bazy wspólnej, gdzie DieticianID == NULL oraz te utworzone przez tego dietetyka) - bez paginacji
        [HttpGet("allNoPagination/{dieticianId}")]
        public async Task<IActionResult> GetDishesNoPagination(int dieticianId)
        {
            var result = await _mediator.Send(new DishesList.Query { DieteticianId = dieticianId });
            return HandleResult(result);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateDish(DishPostDTO dishDto)
        {
            var command = new DishCreate.Command { DishPostDTO = dishDto };
            var result = await _mediator.Send(command);
            if (result.IsSucces)
            {
                return Ok(new { data = result.Value, message = "Danie dodane pomyślnie." });
            }
            return BadRequest(result.Error);
        }

        [HttpGet("filters/{dishId}")]
        public async Task<IActionResult> GetFilters(int dishId)
        {
            var result = await _mediator.Send(new DishFilterList.Query { DishId = dishId });
            return HandleResult(result);
        }

        // DONE : edycja Dish (tylko, gdy nie jest używany w żadnych mealtimetoxyaxis
        [HttpPut("edit/{dishId}")]
        public async Task<IActionResult> EditDish(int dishId, DishEditDTO dishEditDTO)
        {
            var command = new DishEdit.Command
            {
                DishEditDTO = dishEditDTO,
            };
            command.DishEditDTO.Id = dishId;
            var result = await _mediator.Send(command);
            if (result.IsSucces)
            {
                return Ok(new { data = result.Value, message = "Pomyślnie zedytowano danie." });
            }
            return BadRequest(result.Error);
        }

        // DONE : usuwanie dish (deaktywacja dish i powiązanych rzeczy)
        [HttpDelete("delete/{dishId}")]
        public async Task<IActionResult> DeleteDish(int dishId)
        {
            var command = new DishDelete.Command { DishId = dishId };
            var result = await _mediator.Send(command);
            if (result.IsSucces)
            {
                return Ok(new { data = result.Value, message = "Pomyślnie usunięto danie." });
            }
            return BadRequest(result.Error);
        }
    }
}