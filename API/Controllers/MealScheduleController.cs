using Application.CQRS.MealSchedules;
using Application.CQRS.Patients;
using Application.DTOs.MealScheduleDTO;
using Application.DTOs.PatientDTO;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class MealScheduleController : BaseApiController
    {
        [HttpGet("{dietId}")]
        public async Task<ActionResult<List<MealScheduleGetDTO>>> GetDiets(int dietId)
        {

            var result = await Mediator.Send(new MealScheduleDetails.Query { DietId=dietId });
            return HandleResult(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditMealShedule(int id, MealScheduleEditDTO meal)
        {
            var command = new MealSheduleEdit.Command
            {
                MealShedule = meal,
            };
            command.MealShedule.Id = id;

            return HandleResult(await Mediator.Send(command));
        }
    }
}
