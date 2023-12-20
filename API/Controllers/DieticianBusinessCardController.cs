using Application.CQRS.DieticiansBusinessesCards;
using Application.CQRS.Offices;
using Application.DTOs.DieticianBusinessCardDTO;
using Application.DTOs.OfficeDTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class DieticianBusinessCardController : BaseApiController
    {
        public DieticianBusinessCardController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet("details/{dieticianId}")]
        public async Task<ActionResult<DieticianBusinessCardGetDTO>> GetIngredient(int dieticianId)
        {
            var result = await _mediator.Send(new DieticianBusinessCardDetails.Query { DieticianId = dieticianId });
            return HandleResult(result);
        }
    }
}
