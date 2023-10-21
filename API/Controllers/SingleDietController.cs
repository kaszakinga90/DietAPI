using Application.CQRS.SingleDiets;
using Microsoft.AspNetCore.Mvc;
using ModelsDB;
using ModelsDB.Functionality;

namespace API.Controllers
{
    public class SingleDietController : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<List<SingleDiet>>> GetSingleDiets()
        {
            return await Mediator.Send(new SingleDietList.Query());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<SingleDiet>> GetSingleDiet(int id)
        {
            return await Mediator.Send(new SingleDietDetails.Query { Id = id });
        }
        [HttpPost]
        public async Task<IActionResult> CreateSingleDiet(SingleDiet SingleDiet)
        {
            await Mediator.Send(new SingleDietCreate.Command { SingleDiet = SingleDiet });
            return Ok();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> EditSingleDiet(int id, SingleDiet SingleDiet)
        {
            SingleDiet.Id = id;

            await Mediator.Send(new SingleDietEdit.Command { SingleDiet = SingleDiet });
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSingleDiet(int id)
        {
            await Mediator.Send(new SingleDietDelete.Command { Id = id });
            return Ok();
        }
    }
}
