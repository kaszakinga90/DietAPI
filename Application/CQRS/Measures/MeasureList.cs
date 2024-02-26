using Application.Core;
using Application.DTOs.MeasureDTO;
using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Application.CQRS.Measures
{
    public class MeasureList
    {
        public class Query : IRequest<Result<List<MeasureGetDTO>>> { }

        public class Handler : IRequestHandler<Query, Result<List<MeasureGetDTO>>>
        {
            private readonly DietContext _context;

            public Handler(DietContext context)
            {
                _context = context;
            }

            public async Task<Result<List<MeasureGetDTO>>> Handle(Query request, CancellationToken cancellationToken)
            {
                try
                {
                    var measureList = await _context.MeasuresDb
                        .Where(m => m.isActive)
                        .Select(m => new MeasureGetDTO
                        {
                            Id = m.Id,
                            Symbol = m.Symbol,
                            Description = m.Description,
                        })
                        .ToListAsync(cancellationToken);

                    return Result<List<MeasureGetDTO>>.Success(measureList);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Przyczyna niepowodzenia: " + ex);
                    return Result<List<MeasureGetDTO>>.Failure("Wystąpił błąd podczas pobierania lub mapowania danych.");
                }
            }
        }
    }
}