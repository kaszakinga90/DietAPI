using Application.CQRS.Meals;
using Application.DTOs.MealDTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ModelsDB.Functionality;

namespace API.Controllers
{
    public class MealController : BaseApiController
    {
        public MealController(IMediator mediator) : base(mediator)
        {
        }
        [HttpGet]
        public async Task<ActionResult<List<MealGetDTO>>> GetMeals()
        {
            var result = await _mediator.Send(new MealList.Query());
            return HandleResult(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Meal>> GetMeal(int id)
        {
            return await _mediator.Send(new MealDetails.Query { Id = id });
        }
    }
}
