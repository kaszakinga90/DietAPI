using Application.CQRS.Examples;
using Application.DTOs.ExampleDTO;
using Application.Services;
using Microsoft.AspNetCore.Mvc;
using ModelsDB;
using Serilog;

namespace API.Controllers
{
    public class ExampleController : BaseApiController
    {
        private readonly ImageService _imageService;

        public ExampleController(ImageService imageService)
        {
            _imageService = imageService;
        }

        [HttpGet]
        public async Task<IActionResult> GetExamples()
        {
            return HandleResult(await Mediator.Send(new ExampleList.Query()));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetExample(int id)
        {
           var result= await Mediator.Send(new Details.Query { Id = id });

           return HandleResult(result);
        }
        [HttpPost]
        public async Task<IActionResult> CreateExample([FromForm] ExampleDTO exampleDto)

        {
            var example = new Example
            {
                Name = exampleDto.Name,     
                Description = exampleDto.Description,
                Age = exampleDto.Age,
            };
            try
            {
                if (exampleDto.File != null)
                {
                    var imageResult = await _imageService.AddImageAsync(exampleDto.File);

                    if (imageResult.Error != null)
                        return BadRequest(new ProblemDetails { Title = imageResult.Error.Message });

                    example.PictureUrl = imageResult.SecureUrl.ToString();
                    example.PublicId = imageResult.PublicId;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Wystąpił błąd podczas wykonywania operacji.");
                if (ex.InnerException != null)
                {
                    Log.Error(ex.InnerException, "Wewnętrzny wyjątek:");
                }
            }

            return HandleResult(await Mediator.Send(new Create.Command { Example = example }));
        }


        [HttpPut("{id}")]
        public async Task<IActionResult>EditExample(int id,Example example)
        {
            example.Id = id;
            return HandleResult(await Mediator.Send(new Edit.Command {  Example = example }));
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult>DeleteExample(int id)
        {
            return HandleResult(await Mediator.Send(new Delete.Command { Id = id }));
        }
    }
}
