using Application.Core;
using Application.DTOs.CategoryOfDietDTO;
using Application.DTOs.DayWeekDTO;
using AutoMapper;
using DietDB;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.DayWeeks
{
    public class DayWeekDelete
    {
        public class Command : IRequest<Result<DayWeekDeleteDTO>>
        {
            public int Id { get; set; }
            public DayWeekDeleteDTO DayWeekDeleteDTO { get; set; }

        }
        public class Handler : IRequestHandler<Command, Result<DayWeekDeleteDTO>>
        {
            private readonly DietContext _context;
            private readonly IMapper _mapper;

            public Handler(DietContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<DayWeekDeleteDTO>> Handle(Command request, CancellationToken cancellationToken)
            {
                var dayWeek = await _context.DayWeeksDb.FirstOrDefaultAsync(i => i.Id == request.Id);

                if (dayWeek == null)
                {
                    return Result<DayWeekDeleteDTO>.Failure("kategoria diety o podanym ID nie została znaleziona.");
                }

                _mapper.Map(request.DayWeekDeleteDTO, dayWeek);

                dayWeek.isActive = false;

                try
                {
                    var result = await _context.SaveChangesAsync(cancellationToken) > 0;
                    if (!result)
                    {
                        return Result<DayWeekDeleteDTO>.Failure("Usunięcie kategorii nie powiodło się.");
                    }
                }
                catch (Exception ex)
                {
                    return Result<DayWeekDeleteDTO>.Failure("Wystąpił błąd podczas usuwania kategorii.");
                }

                return Result<DayWeekDeleteDTO>.Success(_mapper.Map<DayWeekDeleteDTO>(dayWeek));
            }
        }
    }
}
