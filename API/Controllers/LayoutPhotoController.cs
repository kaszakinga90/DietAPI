using Application.LayoutPhotos;
using Microsoft.AspNetCore.Mvc;
using ModelsDB.Layout;

namespace API.Controllers
{
    public class LayoutPhotoController : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<List<LayoutPhoto>>> GetLayoutPhotos()
        {
            return await Mediator.Send(new LayoutPhotoList.Query());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<LayoutPhoto>> GetLayoutPhoto(int id)
        {
            return await Mediator.Send(new LayoutPhotoDetails.Query { Id = id });
        }
        [HttpPost]
        public async Task<IActionResult> CreateLayoutPhoto(LayoutPhoto LayoutPhoto)
        {
            await Mediator.Send(new LayoutPhotoCreate.Command { LayoutPhoto = LayoutPhoto });
            return Ok();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> EditLayoutPhoto(int id, LayoutPhoto LayoutPhoto)
        {
            LayoutPhoto.Id = id;

            await Mediator.Send(new LayoutPhotoEdit.Command { LayoutPhoto = LayoutPhoto });
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLayoutPhoto(int id)
        {
            await Mediator.Send(new LayoutPhotoDelete.Command { Id = id });
            return Ok();
        }
    }
}
