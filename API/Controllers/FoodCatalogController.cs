using Application.CQRS.FoodCatalogs;
using Application.CQRS.Logos;
using Application.CQRS.Offices;
using Application.DTOs.DishFoodCatalogDTO;
using Application.DTOs.FoodCatalogDTO;
using Application.DTOs.FoodCatalogDTO.Admin;
using Application.DTOs.OfficeDTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class FoodCatalogController : BaseApiController
    {
        public FoodCatalogController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet("getdieticiancatalogs/{dieticianId}")]
        public async Task<IActionResult> GetFoodCatalog(int dieticianId)
        {
            var result = await _mediator.Send(new FoodCatalogDetails.Query { Id = dieticianId });
            return HandleResult(result);
        }

        //pobiera katalogi dań dostępne dla dietetyka (czyli z bazy wspólnej, gdzie DieticianID == NULL oraz te utworzone przez tego dietetyka)
        [HttpGet("getallcatalogs/{dieticianId}")]
        public async Task<IActionResult> GetFoodCatalogs(int dieticianId)
        {
            var result = await _mediator.Send(new FoodCatalogList.Query { DieteticianId = dieticianId });
            return HandleResult(result);
        }

        // ?? - w walidatorze, że dieticianId wymagane - osobno więc dla admina i dietetyka?
        [HttpPost("create")]
        public async Task<IActionResult> CreateFoodCatalog(FoodCatalogPostDTO FoodCatalogPostDTO)
        {
            var result = await _mediator.Send(new FoodCatalogCreate.Command { FoodCatalogPostDTO = FoodCatalogPostDTO });
            return Ok(result.Value);
        }

        // DONE : edycja food catalog dietetyka 
        [HttpPut("edit/{foodCatalogId}")]
        public async Task<IActionResult> EditDieticianFoodCatalog(int foodCatalogId, FoodCatalogDieticianEditDTO foodCatalogDTO)
        {
            var command = new FoodCatalogDieticianEdit.Command
            {
                FoodCatalogDieticianEditDTO = foodCatalogDTO,
            };
            command.FoodCatalogDieticianEditDTO.Id = foodCatalogId;

            return HandleResult(await _mediator.Send(command));
        }

        // DONE : usuwanie foodCatalog 
        // katalog dietetyka - deaktywuje jego katalog i dania z niego przenosi do katalogu Wszystkie (dedykowany katalog dla dietetyka tworzony wraz z rejestracją)
        // katalog wspólny - deaktywuje katalog i dania z niego przenosi do katalogu Ogolny (id = 1) (katalog dostępny dla wszystkich/niemodyfikowalny)

        // na froncie musi być coś w stylu : ta akcja spowoduje przeniesienie wszystkich produktów do katalogu Wszystkie, czy na pewno?
        [HttpDelete("delete/{foodCatalogId}")]
        public async Task<IActionResult> DeleteFoodCatalog(int foodCatalogId, FoodCatalogDeleteDTO foodCatalogDeleteDTO)
        {
            var command = new FoodCatalogDelete.Command { 
                FoodCatalogId = foodCatalogId,
                FoodCatalogDeleteDTO = foodCatalogDeleteDTO
            };

            return HandleResult(await _mediator.Send(command));
        }
    }
}