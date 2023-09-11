using Application.Examples;
using Microsoft.AspNetCore.Mvc;
using ModelsDB;

namespace API.Controllers
{
    public class ExampleController : BaseApiController
    {

        [HttpGet]
        public async Task<ActionResult<List<Example>>> GetExamples()
        {
            return await Mediator.Send(new ExampleList.Query());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Example>> GetExample(int id)
        {
            return await Mediator.Send(new Details.Query { Id = id });
        }
        [HttpPost]
        public async Task<IActionResult> CreateExample(Example example)
        {
            await Mediator.Send(new Create.Command { Example = example });
            return Ok();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult>EditExample(int id,Example example)
        {
            example.Id = id;

            await Mediator.Send(new Edit.Command {  Example = example });
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult>DeleteExample(int id)
        {
            await Mediator.Send(new Delete.Command { Id = id });
            return Ok();
        }
    }
}
