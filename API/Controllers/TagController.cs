using Application.Tags;
using Microsoft.AspNetCore.Mvc;
using ModelsDB.Layout;

namespace API.Controllers
{
    public class TagController : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<List<Tag>>> GetTags()
        {
            return await Mediator.Send(new TagList.Query());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Tag>> GetTag(int id)
        {
            return await Mediator.Send(new TagDetails.Query { Id = id });
        }
        [HttpPost]
        public async Task<IActionResult> CreateTag(Tag Tag)
        {
            await Mediator.Send(new TagCreate.Command { Tag = Tag });
            return Ok();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> EditTag(int id, Tag Tag)
        {
            Tag.Id = id;

            await Mediator.Send(new TagEdit.Command { Tag = Tag });
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTag(int id)
        {
            await Mediator.Send(new TagDelete.Command { Id = id });
            return Ok();
        }
    }
}
