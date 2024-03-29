﻿using Application.CQRS.Ingredients;
using Application.DTOs.IngredientDTO;
using Application.FiltersExtensions.Ingredients;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize(Roles = "SuperAdmin, Admin, Dietetician")]
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

        [Authorize(Roles = "SuperAdmin, Admin, Dietetician")]
        [HttpPost("create")]
        public async Task<IActionResult> CreateIngredient([FromForm] IngredientDTO IngredientDTO, [FromForm] IFormFile file)
        {
            var command = new IngredientCreate.Command
            {
                IngredientDTO = IngredientDTO,
                File = file,
            };
            var result = await _mediator.Send(command);
            if (result.IsSucces)
            {
                return Ok(new { data = result.Value, message = "Pomyślnie dodano składnik." });
            }
            return BadRequest(result.Error);
        }

        [Authorize(Roles = "SuperAdmin, Admin, Dietetician")]
        [HttpPut("edit/{ingredientId}")]
        public async Task<IActionResult> EditIngredient(int ingredientId, IngredientEditDTO ingredientEditDTO)
        {
            var command = new IngredientEdit.Command { IngredientEditDTO = ingredientEditDTO };
            command.IngredientEditDTO.Id = ingredientId;

            var result = await _mediator.Send(command);
            if (result.IsSucces)
            {
                return Ok(new { data = result.Value, message = "Pomyślnie zedytowano składnik." });
            }
            return BadRequest(result.Error);
        }

        [HttpDelete("delete/{ingredientId}")]
        public async Task<IActionResult> DeleteIngredient(int ingredientId)
        {
            var command = new IngredientDelete.Command { IngredientId = ingredientId };

            var result = await _mediator.Send(command);
            if (result.IsSucces)
            {
                return Ok(new { data = result.Value, message = "Pomyślnie usunięto składnik." });
            }
            return BadRequest(result.Error);
        }
    }
}