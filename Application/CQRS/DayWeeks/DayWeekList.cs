using Application.Core;
using Application.DTOs.DayWeekDTO;
using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Application.CQRS.DayWeeks
{
    public class DayWeekList
    {
        public class Query : IRequest<Result<List<DayWeekGetDTO>>> { }

        public class Handler : IRequestHandler<Query, Result<List<DayWeekGetDTO>>>
        {
            private readonly DietContext _context;

            public Handler(DietContext context)
            {
                _context = context;
            }

            public async Task<Result<List<DayWeekGetDTO>>> Handle(Query request, CancellationToken cancellationToken)
            {
                try
                {
                    var dayWeeksList = await _context.DayWeeksDb
                    .Select(m => new DayWeekGetDTO
                    {
                        Id = m.Id,
                        Day = m.Day,
                    })
                    .ToListAsync(cancellationToken);

                    return Result<List<DayWeekGetDTO>>.Success(dayWeeksList);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Przyczyna niepowodzenia: " + ex);
                    return Result<List<DayWeekGetDTO>>.Failure("Wystąpił błąd podczas pobierania lub mapowania danych.");
                }
            }
        }
    }
}