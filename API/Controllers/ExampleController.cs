using Application.CQRS.Examples;
using Microsoft.AspNetCore.Mvc;
using ModelsDB;

namespace API.Controllers
{
    public class ExampleController : BaseApiController
    {

        //[HttpGet]
        //public async Task<IActionResult> GetExamples()
        //{
        //    return HandleResult(await Mediator.Send(new ExampleList.Query()));
        //}
        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetExample(int id)
        //{
        //   var result= await Mediator.Send(new Details.Query { Id = id });

        //   return HandleResult(result);
        //}
        //[HttpPost]
        //public async Task<IActionResult> CreateExample(Example example)
        //{
        //    return HandleResult(await Mediator.Send(new Create.Command { Example = example }));
        //}
        //[HttpPut("{id}")]
        //public async Task<IActionResult>EditExample(int id,Example example)
        //{
        //    example.Id = id;
        //    return HandleResult(await Mediator.Send(new Edit.Command {  Example = example }));
        //}
        //[HttpDelete("{id}")]
        //public async Task<IActionResult>DeleteExample(int id)
        //{
        //    return HandleResult(await Mediator.Send(new Delete.Command { Id = id }));
        //}
    }
}
