using Application.CQRS.CategoryOfDiets;
using Application.DTOs.CategoryOfDietDTO;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModelsDB.Functionality;

namespace API.Controllers
{
    [Authorize(Roles = "SuperAdmin, Admin, Dietetician, Patient")]
    public class CategoryOfDietController : BaseApiController
    {
        public CategoryOfDietController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        public async Task<ActionResult<List<CategoryOfDiet>>> GetCategoriesOfDiets()
        {
            return await _mediator.Send(new CategoryOfDietList.Query());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryOfDiet>> GetCategoryOfDiet(int id)
        {
            return await _mediator.Send(new CategoryOfDietDetails.Query { Id = id });
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategoryOfDiet(CategoryOfDiet CategoryOfDiet)
        {
            await _mediator.Send(new CategoryOfDietCreate.Command { CategoryOfDiet = CategoryOfDiet });
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditCategoryOfDiet(int id, CategoryOfDiet CategoryOfDiet)
        {
            CategoryOfDiet.Id = id;

            await _mediator.Send(new CategoryOfDietEdit.Command { CategoryOfDiet = CategoryOfDiet });
            return Ok();
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteCategoryOfDiet(int id, CategoryOfDietDeleteDTO category)
        {
            var command = new CategoryOfDietDelete.Command
            {
                Id = id,
                CategoryOfDietDeleteDTO = category,
            };
            return HandleResult(await _mediator.Send(command));
        }
    }
}
