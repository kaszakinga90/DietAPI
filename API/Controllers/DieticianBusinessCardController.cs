using Application.CQRS.DieticiansBusinessesCards;
using Application.CQRS.Specializations;
using Application.DTOs.DieticianBusinessCardDTO;
using Application.FiltersExtensions.DieticianBussinesCards;
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
        [HttpGet("allnopagination")]
        public async Task<IActionResult> GetBusinessCardsNoPagination()
        {
            var result = await _mediator.Send(new DieticianBusinessCardsNoPaginationList.Query());
            return HandleResult(result);
        }

        [HttpGet("details/{dieticianId}")]
        public async Task<ActionResult<DieticianBusinessCardGetDTO>> GetDieticianBusinessCard(int dieticianId)
        {
            var result = await _mediator.Send(new DieticianBusinessCardDetails.Query { DieticianId = dieticianId });
            return HandleResult(result);
        }

        [HttpGet("filters/{dieticianId}")]
        public async Task<ActionResult<BusinessCardFiltersDTO>> GetFilters(int dieticianId)
        {
            var result = await _mediator.Send(new DieticianBusinessCardFilterList.Query { DieticianId = dieticianId });
            return HandleResult(result);
        }
    }
}
