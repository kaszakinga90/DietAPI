using Application.CQRS.Ingredients;
using Application.CQRS.Meals;
using Application.DTOs.IngredientDTO;
using Application.Services;
using DietDB;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ModelsDB;
using ModelsDB.Functionality;

namespace API.Controllers
{
    public class IngredientController : BaseApiController
    {
        private readonly ImageService _imageService;
        private readonly DietContext _context;

        public IngredientController(ImageService imageService, DietContext context)
        {
            _imageService = imageService;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Ingredient>>> GetIngredients(int? dietitianId)
        {
            try
            {
                var query = new IngredientList.Query(dietitianId);
                var ingredients = await Mediator.Send(query);

                if (ingredients == null || ingredients.Count == 0)
                {
                    return NotFound(); // Zwróć NotFound, jeśli nie znaleziono składników
                }

                return Ok(ingredients);
            }
            catch (Exception ex)
            {
                // Tutaj możesz obsłużyć wyjątek, zalogować go, itp.
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IngredientGetDTO>> GetIngredient(int id)
        {
            return await Mediator.Send(new IngredientDetails.Query { Id = id });
        }

        [HttpPost]
        public async Task<IActionResult> CreateIngredient(IngredientDTO IngredientDTO)
        {
            return HandleResult(await Mediator.Send(new IngredientCreate.Command { IngredientDTO = IngredientDTO }));
        }
    }
}