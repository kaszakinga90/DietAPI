using Application.CQRS.Diplomas;
using Application.CQRS.Patients;
using Application.DTOs.DiplomaDTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class DiplomaController : BaseApiController
    {
        [HttpGet("{dieticianId}")]
        public async Task<IActionResult> GetDiplomas(int dieticianId)
        {
            var result = await Mediator.Send(new DiplomasDieticianList.Query { DieticianId = dieticianId });
            return HandleResult(result);
        }
    }


}