using Application.LayoutCategories;
using Microsoft.AspNetCore.Mvc;
using ModelsDB.Layout;

namespace API.Controllers
{
    public class LayoutCategoryController : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<List<LayoutCategory>>> GetLayoutCategorys()
        {
            return await Mediator.Send(new LayoutCategoryList.Query());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<LayoutCategory>> GetLayoutCategory(int id)
        {
            return await Mediator.Send(new LayoutCategoryDetails.Query { Id = id });
        }
        [HttpPost]
        public async Task<IActionResult> CreateLayoutCategory(LayoutCategory LayoutCategory)
        {
            await Mediator.Send(new LayoutCategoryCreate.Command { LayoutCategory = LayoutCategory });
            return Ok();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> EditLayoutCategory(int id, LayoutCategory LayoutCategory)
        {
            LayoutCategory.Id = id;

            await Mediator.Send(new LayoutCategoryEdit.Command { LayoutCategory = LayoutCategory });
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLayoutCategory(int id)
        {
            await Mediator.Send(new LayoutCategoryDelete.Command { Id = id });
            return Ok();
        }
    }
}
