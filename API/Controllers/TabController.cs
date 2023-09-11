using Application.Tabs;
using Microsoft.AspNetCore.Mvc;
using ModelsDB.Layout;

namespace API.Controllers
{
    public class TabController : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<List<Tab>>> GetTabs()
        {
            return await Mediator.Send(new TabList.Query());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Tab>> GetTab(int id)
        {
            return await Mediator.Send(new TabDetails.Query { Id = id });
        }
        [HttpPost]
        public async Task<IActionResult> CreateTab(Tab Tab)
        {
            await Mediator.Send(new TabCreate.Command { Tab = Tab });
            return Ok();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> EditTab(int id, Tab Tab)
        {
            Tab.Id = id;

            await Mediator.Send(new TabEdit.Command { Tab = Tab });
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTab(int id)
        {
            await Mediator.Send(new TabDelete.Command { Id = id });
            return Ok();
        }
    }
}
