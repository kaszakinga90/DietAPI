using Application.Core;
using Application.DTOs.DayWeekDTO;
using AutoMapper;
using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Application.CQRS.DayWeekDTOs
{
    public class DayWeekDetails
    {
        public class Query : IRequest<Result<DayWeekGetDTO>>
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<DayWeekGetDTO>>
        {
            private readonly DietContext _context;
            private readonly IMapper _mapper;

            public Handler(DietContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<DayWeekGetDTO>> Handle(Query request, CancellationToken cancellationToken)
            {
                try
                {
                    var dayWeek = await _context.DayWeeksDb
                    .SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

                    if (dayWeek == null)
                    {
                        return Result<DayWeekGetDTO>.Failure("Day week nie został znaleziony.");
                    }

                    return Result<DayWeekGetDTO>.Success(_mapper.Map<DayWeekGetDTO>(dayWeek));
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Przyczyna niepowodzenia: " + ex);
                    return Result<DayWeekGetDTO>.Failure("Wystąpił błąd podczas pobierania lub mapowania danych.");
                } 
            }
        }
    }
}