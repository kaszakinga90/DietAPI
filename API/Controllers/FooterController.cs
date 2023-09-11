using Application.Footers;
using Microsoft.AspNetCore.Mvc;
using ModelsDB.Layout;

namespace API.Controllers
{
    public class FooterController : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<List<Footer>>> GetFooters()
        {
            return await Mediator.Send(new FooterList.Query());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Footer>> GetFooter(int id)
        {
            return await Mediator.Send(new FooterDetails.Query { Id = id });
        }
        [HttpPost]
        public async Task<IActionResult> CreateFooter(Footer Footer)
        {
            await Mediator.Send(new FooterCreate.Command { Footer = Footer });
            return Ok();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> EditFooter(int id, Footer Footer)
        {
            Footer.Id = id;

            await Mediator.Send(new FooterEdit.Command { Footer = Footer });
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFooter(int id)
        {
            await Mediator.Send(new FooterDelete.Command { Id = id });
            return Ok();
        }
    }
}
