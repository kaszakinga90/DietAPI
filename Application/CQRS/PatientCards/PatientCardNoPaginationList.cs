using Application.Core;
using Application.DTOs.PatientCardDTO;
using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Application.CQRS.PatientCards
{
    public class PatientCardNoPaginationList
    {
        public class Query : IRequest<Result<List<PatientCardGetDTO>>>
        {
            public int PatientId { get; set; }
            public class Handler : IRequestHandler<Query, Result<List<PatientCardGetDTO>>>
            {
                private readonly DietContext _context;

                public Handler(DietContext context)
                {
                    _context = context;
                }

                public async Task<Result<List<PatientCardGetDTO>>> Handle(Query request, CancellationToken cancellationToken)
                {
                    try
                    {
                        var patientCardList =await _context.PatientCardsDb
                            .Where(m => m.PatientId == request.PatientId && m.isActive)
                            .Select(m => new PatientCardGetDTO
                            {
                                Id = m.Id,
                                PatientId = m.PatientId,
                                DieticianName = m.Dietician.FirstName + " " + m.Dietician.LastName,
                            })
                            .ToListAsync(cancellationToken);

                        return Result<List<PatientCardGetDTO>>.Success(patientCardList);
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("Przyczyna niepowodzenia: " + ex);
                        return Result<List<PatientCardGetDTO>>.Failure("Wystąpił błąd podczas pobierania lub mapowania danych.");
                    }
                }
            }
        }
    }
}