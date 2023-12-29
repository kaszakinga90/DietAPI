using Application.CQRS.DieticiansBusinessesCards;
using Application.CQRS.Offices;
using Application.DTOs.DieticianBusinessCardDTO;
using Application.DTOs.OfficeDTO;
using Application.FiltersExtensions.DieticianBussinesCards;
using Application.FiltersExtensions.Diets;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class DieticianBusinessCardController : BaseApiController
    {
        public DieticianBusinessCardController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetBusinessCards([FromQuery] DieticianBussinesCardsParams pagingParams)
        {
            var result = await _mediator.Send(new DieticianBusinessCardList.Query { Params = pagingParams });
            return HandlePagedResult(result);
        }

        [HttpGet("details/{dieticianId}")]
        public async Task<ActionResult<DieticianBusinessCardGetDTO>> GetDieticianBusinessCard(int dieticianId)
        {
            var result = await _mediator.Send(new DieticianBusinessCardDetails.Query { DieticianId = dieticianId });
            return HandleResult(result);
        }
    }
}
