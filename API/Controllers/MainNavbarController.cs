using Application.MainNavbars;
using Microsoft.AspNetCore.Mvc;
using ModelsDB.Layout;

namespace API.Controllers
{
    public class MainNavbarController : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<List<MainNavbar>>> GetMainNavbars()
        {
            return await Mediator.Send(new MainNavbarList.Query());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<MainNavbar>> GetMainNavbar(int id)
        {
            return await Mediator.Send(new MainNavbarDetails.Query { Id = id });
        }
        [HttpPost]
        public async Task<IActionResult> CreateMainNavbar(MainNavbar MainNavbar)
        {
            await Mediator.Send(new MainNavbarCreate.Command { MainNavbar = MainNavbar });
            return Ok();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> EditMainNavbar(int id, MainNavbar MainNavbar)
        {
            MainNavbar.Id = id;

            await Mediator.Send(new MainNavbarEdit.Command { MainNavbar = MainNavbar });
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMainNavbar(int id)
        {
            await Mediator.Send(new MainNavbarDelete.Command { Id = id });
            return Ok();
        }
    }
}
