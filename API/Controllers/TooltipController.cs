using Application.Tooltips;
using Microsoft.AspNetCore.Mvc;
using ModelsDB;
using ModelsDB.ManualPanel;

namespace API.Controllers
{
    public class TooltipController : BaseApiController
    {

        [HttpGet]
        public async Task<ActionResult<List<Tooltip>>> GetTooltips()
        {
            return await Mediator.Send(new TooltipList.Query());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Tooltip>> GetTooltip(int id)
        {
            return await Mediator.Send(new TooltipDetails.Query { Id = id });
        }
        [HttpPost]
        public async Task<IActionResult> CreateTooltip(Tooltip Tooltip)
        {
            await Mediator.Send(new TooltipCreate.Command { Tooltip = Tooltip });
            return Ok();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> EditTooltip(int id, Tooltip Tooltip)
        {
            Tooltip.Id = id;

            await Mediator.Send(new TooltipEdit.Command { Tooltip = Tooltip });
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTooltip(int id)
        {
            await Mediator.Send(new TooltipDelete.Command { Id = id });
            return Ok();
        }
    }
}
