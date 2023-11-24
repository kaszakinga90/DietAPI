using Application.CQRS.Meals;
using Application.CQRS.Specializations;
using Microsoft.AspNetCore.Mvc;
using ModelsDB.Functionality;

namespace API.Controllers
{
    public class MealController : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<List<Meal>>> GetMeals()
        {
            var result = await Mediator.Send(new MealList.Query());
            return HandleResult(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Meal>> GetMeal(int id)
        {
            return await Mediator.Send(new MealDetails.Query { Id = id });
        }
    }
}
