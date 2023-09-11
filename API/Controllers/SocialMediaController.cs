using Application.SocialMedias;
using Microsoft.AspNetCore.Mvc;
using ModelsDB.Layout;

namespace API.Controllers
{
    public class SocialMediaController : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<List<SocialMedia>>> GetSocialMedias()
        {
            return await Mediator.Send(new SocialMediaList.Query());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<SocialMedia>> GetSocialMedia(int id)
        {
            return await Mediator.Send(new SocialMediaDetails.Query { Id = id });
        }
        [HttpPost]
        public async Task<IActionResult> CreateSocialMedia(SocialMedia SocialMedia)
        {
            await Mediator.Send(new SocialMediaCreate.Command { SocialMedia = SocialMedia });
            return Ok();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> EditSocialMedia(int id, SocialMedia SocialMedia)
        {
            SocialMedia.Id = id;

            await Mediator.Send(new SocialMediaEdit.Command { SocialMedia = SocialMedia });
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSocialMedia(int id)
        {
            await Mediator.Send(new SocialMediaDelete.Command { Id = id });
            return Ok();
        }
    }
}
