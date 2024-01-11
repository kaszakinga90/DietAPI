using Application.CQRS.Meals;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class MealController : BaseApiController
    {
        public MealController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetMeals()
        {
            var result = await _mediator.Send(new MealList.Query());
            return HandleResult(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMeal(int id)
        {
            var result = await _mediator.Send(new MealDetails.Query { Id = id });
            return HandleResult(result);
        }
    }
}