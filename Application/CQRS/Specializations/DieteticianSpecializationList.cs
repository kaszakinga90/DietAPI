using Application.Core;
using Application.DTOs.SpecializationDTO;
using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Application.CQRS.Specializations
{
    public class DieteticianSpecializationList
    {
        public class Query : IRequest<Result<List<DieteticianSpecializationGetDTO>>>
        {
            public int DieticianId { get; set; }
            public class Handler : IRequestHandler<Query, Result<List<DieteticianSpecializationGetDTO>>>
            {
                private readonly DietContext _context;

                public Handler(DietContext context)
                {
                    _context = context;
                }

                public async Task<Result<List<DieteticianSpecializationGetDTO>>> Handle(Query request, CancellationToken cancellationToken)
                {
                    try
                    {
                        var dieticianSpecializationsList = await _context.DieticianSpecialization
                        .Where(m => m.DieticianId == request.DieticianId && m.isActive)
                        .Select(m => new DieteticianSpecializationGetDTO
                        {
                            DieticianId = m.DieticianId,
                            SpecializationId = m.SpecializationId,
                            SpecializationName = m.Specialization.SpecializationName,
                            DieticianName = m.Dietician.FirstName + " " + m.Dietician.LastName,
                        })
                        .ToListAsync(cancellationToken);

                        return Result<List<DieteticianSpecializationGetDTO>>.Success(dieticianSpecializationsList);
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("Przyczyna niepowodzenia: " + ex);
                        return Result<List<DieteticianSpecializationGetDTO>>.Failure("Wystąpił błąd podczas pobierania lub mapowania danych.");
                    }
                }
            }
        }
    }
}