using Application.CQRS.MealsTimesToXYAxiss;
using Application.DTOs.MealTimeToXYAxisDTO;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class MealTimeToXYAxisController : BaseApiController
    {
        public MealTimeToXYAxisController(IMediator mediator):base(mediator)
        {
        }

        [HttpGet("details/{dietId}")]
        public async Task<ActionResult<List<MealTimeToXYAxisGetDTO>>> GetDiets(int dietId)
        {
            var result = await _mediator.Send(new MealTimeToXYAxisDetails.Query { DietId = dietId });
            return HandleResult(result);
        }

        [Authorize(Roles = "SuperAdmin, Admin, Dietetician")]
        [HttpPut("edit/{id}")]
        public async Task<IActionResult> EditMealShedule(int id, MealTimeToXYAxisEditDTO meal)
        {
            var command = new MealTimeToXYAxisEdit.Command
            {
                MealTimeToXYAxisEditDTO = meal,
            };
            command.MealTimeToXYAxisEditDTO.Id = id;

            var result = await _mediator.Send(command);
            if (result.IsSucces)
            {
                return Ok(new { data = result.Value, message = "Pomyślnie zedytowano mealtime." });
            }
            return BadRequest(result.Error);
        }
    }
}