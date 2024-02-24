using Application.CQRS.DieticiansBusinessesCards;
using Application.FiltersExtensions.DieticianBussinesCards;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    //[Authorize(Roles = "SuperAdmin, Admin, Dietetician, Patient")]
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
        public async Task<IActionResult> GetDieticianBusinessCard(int dieticianId)
        {
            var result = await _mediator.Send(new DieticianBusinessCardDetails.Query { DieticianId = dieticianId });
            return HandleResult(result);
        }

        [HttpGet("filters")]
        public async Task<IActionResult> GetFilters()
        {
            var result = await _mediator.Send(new DieticianBusinessCardFilterList.Query());
            return HandleResult(result);
        }
    }
}