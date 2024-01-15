﻿using Application.CQRS.FoodCatalogs;
using Application.DTOs.FoodCatalogDTO;
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

        [HttpPost("create")]
        public async Task<IActionResult> CreateFoodCatalog(FoodCatalogPostDTO FoodCatalogPostDTO)
        {
            var result = await _mediator.Send(new FoodCatalogCreate.Command { FoodCatalogPostDTO = FoodCatalogPostDTO });
            return Ok(result.Value);
        }

        // TODO: edycja

        // TODO: delete, ale jeśli będzie funkcja przenieś do innego folderu i dopiero można usunąć, jeśli folder pusty
    }
}