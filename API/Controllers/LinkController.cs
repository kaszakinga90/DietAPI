using Application.Links;
using Microsoft.AspNetCore.Mvc;
using ModelsDB.Layout;

namespace API.Controllers
{
    public class LinkController : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<List<Link>>> GetLinks()
        {
            return await Mediator.Send(new LinkList.Query());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Link>> GetLink(int id)
        {
            return await Mediator.Send(new LinkDetails.Query { Id = id });
        }
        [HttpPost]
        public async Task<IActionResult> CreateLink(Link Link)
        {
            await Mediator.Send(new LinkCreate.Command { Link = Link });
            return Ok();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> EditLink(int id, Link Link)
        {
            Link.Id = id;

            await Mediator.Send(new LinkEdit.Command { Link = Link });
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLink(int id)
        {
            await Mediator.Send(new LinkDelete.Command { Id = id });
            return Ok();
        }
    }
}
