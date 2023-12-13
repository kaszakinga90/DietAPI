using Application.CQRS.MealsTimesToXYAxiss;
using Application.DTOs.MealTimeToXYAxisDTO;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class MealTimeToXYAxisController : BaseApiController
    {
        [HttpGet("details/{dietId}")]
        public async Task<ActionResult<List<MealTimeToXYAxisGetDTO>>> GetDiets(int dietId)
        {

            var result = await Mediator.Send(new MealTimeToXYAxisDetails.Query { DietId = dietId });
            return HandleResult(result);
        }

        [HttpPut("edit/{id}")]
        public async Task<IActionResult> EditMealShedule(int id, MealTimeToXYAxisEditDTO meal)
        {
            var command = new MealTimeToXYAxisEdit.Command
            {
                MealTimeToXYAxis = meal,
            };
            command.MealTimeToXYAxis.Id = id;

            return HandleResult(await Mediator.Send(command));
        }
    }
}
