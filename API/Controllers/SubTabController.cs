using Application.SubTabs;
using Microsoft.AspNetCore.Mvc;
using ModelsDB.Layout;

namespace API.Controllers
{
    public class SubTabController : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<List<SubTab>>> GetSubTabs()
        {
            return await Mediator.Send(new SubTabList.Query());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<SubTab>> GetSubTab(int id)
        {
            return await Mediator.Send(new SubTabDetails.Query { Id = id });
        }
        [HttpPost]
        public async Task<IActionResult> CreateSubTab(SubTab SubTab)
        {
            await Mediator.Send(new SubTabCreate.Command { SubTab = SubTab });
            return Ok();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> EditSubTab(int id, SubTab SubTab)
        {
            SubTab.Id = id;

            await Mediator.Send(new SubTabEdit.Command { SubTab = SubTab });
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubTab(int id)
        {
            await Mediator.Send(new SubTabDelete.Command { Id = id });
            return Ok();
        }
    }
}
