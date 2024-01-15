using Application.CQRS.MealsTimesToXYAxiss;
using Application.DTOs.MealTimeToXYAxisDTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class MealTimeToXYAxisController : BaseApiController
    {
        public MealTimeToXYAxisController(IMediator mediator):base(mediator)
        {
        }

        //[HttpGet("details/{dietId}")]
        //public async Task<IActionResult> GetDiets(int dietId)
        //{
        //    // TODO : referencje dla nazwy MealTimeToXYAxisDetails - bo to jest lista
        //    var result = await _mediator.Send(new MealTimeToXYAxisDetails.Query { DietId = dietId });
        //    return HandleResult(result);
        //}
        [HttpGet("details/{dietId}")]
        public async Task<ActionResult<List<MealTimeToXYAxisGetDTO>>> GetDiets(int dietId)
        {

            var result = await _mediator.Send(new MealTimeToXYAxisDetails.Query { DietId = dietId });
            return HandleResult(result);
        }


        [HttpPut("edit/{id}")]
        public async Task<IActionResult> EditMealShedule(int id, MealTimeToXYAxisEditDTO meal)
        {
            var command = new MealTimeToXYAxisEdit.Command
            {
                MealTimeToXYAxisEditDTO = meal,
            };
            command.MealTimeToXYAxisEditDTO.Id = id;

            return HandleResult(await _mediator.Send(command));
        }
    }
}