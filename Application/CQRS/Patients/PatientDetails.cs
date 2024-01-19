using Application.Core;
using AutoMapper;
using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Application.CQRS.Patients
{
    public class PatientDetails
    {
        public class Query : IRequest<Result<PatientGetDTO>>
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<PatientGetDTO>>
        {
            private readonly DietContext _context;
            private readonly IMapper _mapper;

            public Handler(DietContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<PatientGetDTO>> Handle(Query request, CancellationToken cancellationToken)
            {
                try
                {
                    var patient = await _context.PatientsDb
                          .Include(p => p.Address)
                          .Include(p => p.MessageTo)
                          .SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

                    if (patient == null)
                    {
                        return Result<PatientGetDTO>.Failure("Pacjent nie został znaleziony.");
                    }

                    return Result<PatientGetDTO>.Success(_mapper.Map<PatientGetDTO>(patient));
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Przyczyna niepowodzenia: " + ex);
                    return Result<PatientGetDTO>.Failure("Wystąpił błąd podczas pobierania lub mapowania danych.");
                }
            }
        }
    }
}