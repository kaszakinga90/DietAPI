using Application.CQRS.PatientCards;
using Application.DTOs.PatientCardDTO;
using AutoMapper;
using DietDB;
using Microsoft.AspNetCore.Mvc;
using ModelsDB;

namespace API.Controllers
{
    public class PatientCardController : BaseApiController
    {
        private readonly DietContext _context;
        private readonly IMapper _mapper;

        public PatientCardController(DietContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // IMPORTANT: FROM SQL
        [HttpPost("create")]
        public async Task<ActionResult<PatientCard>> CreatePatientCardCQRS(PatientCardPostDTO pc, int patientId, int dieticianId, int sexId)
        {
            await Mediator.Send(new PatientCardCreate.Command { PatientCardPostDTO = pc, PatientId = patientId, DieticianId = dieticianId, SexId = sexId });
            return Ok();
        }

    }
}
