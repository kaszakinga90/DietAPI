using Application.DTOs.PatientCardDTO;
using AutoMapper;
using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.PatientCards
{
    public class PatientCardDetails
    {
        public class Query : IRequest<PatientCardGetDTO>
        {
            public int PatientId { get; set; }
            public int DieticianId { get; set; }
        }

        public class Handler : IRequestHandler<Query, PatientCardGetDTO>
        {
            private readonly DietContext _context;
            private readonly IMapper _mapper;

            public Handler(DietContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<PatientCardGetDTO> Handle(Query request, CancellationToken cancellationToken)
            {
                var patientCard = await _context.PatientCardsDb.FirstOrDefaultAsync(x => x.DieticianId == request.DieticianId && x.PatientId == request.PatientId, cancellationToken);
                return _mapper.Map<PatientCardGetDTO>(patientCard);
            }
        }
    }
}
