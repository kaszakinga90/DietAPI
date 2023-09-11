using Application.CategoryOfDiets;
using Microsoft.AspNetCore.Mvc;
using ModelsDB.Functionality;

namespace API.Controllers
{
    public class CategoryOfDietController : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<List<CategoryOfDiet>>> GetCategoriesOfDiets()
        {
            return await Mediator.Send(new CategoryOfDietList.Query());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryOfDiet>> GetCategoryOfDiet(int id)
        {
            return await Mediator.Send(new CategoryOfDietDetails.Query { Id = id });
        }
        [HttpPost]
        public async Task<IActionResult> CreateCategoryOfDiet(CategoryOfDiet CategoryOfDiet)
        {
            await Mediator.Send(new CategoryOfDietCreate.Command { CategoryOfDiet = CategoryOfDiet });
            return Ok();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> EditCategoryOfDiet(int id, CategoryOfDiet CategoryOfDiet)
        {
            CategoryOfDiet.Id = id;

            await Mediator.Send(new CategoryOfDietEdit.Command { CategoryOfDiet = CategoryOfDiet });
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoryOfDiet(int id)
        {
            await Mediator.Send(new CategoryOfDietDelete.Command { Id = id });
            return Ok();
        }
    }
}
