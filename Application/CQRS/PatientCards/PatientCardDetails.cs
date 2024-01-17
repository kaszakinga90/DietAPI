using Application.Core;
using Application.DTOs.PatientCardDTO;
using AutoMapper;
using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Application.CQRS.PatientCards
{
    public class PatientCardDetails
    {
        public class Query : IRequest<Result<PatientCardGetDTO>>
        {
            public int PatientId { get; set; }
            public int DieticianId { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<PatientCardGetDTO>>
        {
            private readonly DietContext _context;
            private readonly IMapper _mapper;

            public Handler(DietContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<PatientCardGetDTO>> Handle(Query request, CancellationToken cancellationToken)
            {
                try
                {
                    var patientCard = await _context.PatientCardsDb
                        .FirstOrDefaultAsync(x => x.DieticianId == request.DieticianId && x.PatientId == request.PatientId, cancellationToken);

                    if (patientCard == null)
                    {
                        return Result<PatientCardGetDTO>.Failure("Karta pacjenta nie została znaleziona.");
                    }

                    return Result<PatientCardGetDTO>.Success(_mapper.Map<PatientCardGetDTO>(patientCard));
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Przyczyna niepowodzenia: " + ex);
                    return Result<PatientCardGetDTO>.Failure("Wystąpił błąd podczas pobierania lub mapowania danych.");
                }
            }
        }
    }
}